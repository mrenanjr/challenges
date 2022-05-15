using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.Common;
using TemplateS.Application.Interfaces;
using TemplateS.Application.ViewModels;
using TemplateS.Application.ViewModels.Request;
using TemplateS.Application.ViewModels.Response;
using TemplateS.Domain.Entities;
using TemplateS.Domain.Interfaces;
using TemplateS.Infra.CrossCutting.Auth.Services;

namespace TemplateS.Application.Services
{
    public class UserService : BaseService<UserService>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IMapper mapper) : base(logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetAllResponse<UserViewModel>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return new GetAllResponse<UserViewModel>() {
                Datas = users != null ? users.Select(a => _mapper.Map<UserViewModel>(a)).ToList() : new List<UserViewModel>()
            };
        }

        public async Task<GetResponse<UserViewModel>> GetByIdAsync(string id)
        {
            var response = new GetResponse<UserViewModel>();
            var guid = ValidationService.ValidGuid<User>(id);
            var user = await _userRepository.GetAsync(x => x.Id == guid);

            if (user != null) response.Data = _mapper.Map<UserViewModel>(user);

            return response;
        }

        public async Task<CreateResponse<UserViewModel>> CreateAsync(CreateUserRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);

            var user = _mapper.Map<User>(viewModel);
            var newUser = await _userRepository.CreateAsync(user);

            return new CreateResponse<UserViewModel>() { Data = _mapper.Map<UserViewModel>(newUser) };
        }

        public async Task<UpdateResponse> UpdateAsync(string id, UpdateUserRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);

            var guid = ValidationService.ValidGuid<User>(id);
            var user = _userRepository.Find(x => x.Id == guid);

            ValidationService.ValidExists(user);

            _mapper.Map(viewModel, user);

            await _userRepository.UpdateAsync(user);

            return new UpdateResponse();
        }

        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var guid = ValidationService.ValidGuid<User>(id);
            var user = _userRepository.Find(x => x.Id == guid);

            ValidationService.ValidExists(user);

            await _userRepository.DeleteAsync(user);

            return new DeleteResponse();
        }

        public UserAuthResponseViewModel Authenticate(UserAuthRequestViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Email) || string.IsNullOrEmpty(viewModel.Password))
                throw new Exception("Email/Password are required.");

            viewModel.Password = EncryptPassword(viewModel.Password);

            var user = _userRepository.Find(x => x.Email.ToLower() == viewModel.Email.ToLower() && x.Password.ToLower() == viewModel.Password.ToLower());
            ValidationService.ValidExists(user);

            return new UserAuthResponseViewModel(_mapper.Map<UserViewModel>(user), TokenService.GenerateToken(user));
        }

        private string EncryptPassword(string password)
        {
            HashAlgorithm sha = new SHA1CryptoServiceProvider();

            byte[] encryptedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var caracter in encryptedPassword)
                stringBuilder.Append(caracter.ToString("X2"));

            return stringBuilder.ToString();
        }
    }
}
