using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.AutoMapper;
using TemplateS.Application.Services;
using TemplateS.Application.ViewModels;
using TemplateS.Application.ViewModels.Request;
using TemplateS.Domain.Entities;
using TemplateS.Domain.Interfaces;
using TemplateS.Infra.CrossCutting.ExceptionHandler.Extensions;
using Xunit;

namespace TemplateS.Application.Tests.Services
{
    public class UserServiceTests
    {
        private UserService _userService;
        private IMapper _mapper;

        public UserServiceTests()
        {
            var autoMapperProfile = new AutoMapperSetup();
            var configuration = new MapperConfiguration(x => x.AddProfile(autoMapperProfile));
            _mapper = new Mapper(configuration);
            _userService = new UserService(new Mock<ILogger<UserService>>().Object, new Mock<IUserRepository>().Object, _mapper);
        }

        #region ValidatingSendingID

        [Fact]
        public async Task GetById_SendingEmptyGuid()
        {
            var exception = await Assert.ThrowsAsync<ApiException>(() => _userService.GetByIdAsync(""));
            Assert.Equal("User is not valid", exception.Message);
        }

        [Fact]
        public async Task Update_SendingInvalidId()
        {
            var exception = await Assert.ThrowsAsync<ApiException>(() => _userService.UpdateAsync("", new UpdateUserRequestViewModel() { Name = "Admin", Email = "admin@gmail.com", Password = "123456" }));
            Assert.Equal("User is not valid", exception.Message);
        }

        [Fact]
        public async Task Delete_SendingEmptyGuid()
        {
            var exception = await Assert.ThrowsAsync<ApiException>(() => _userService.DeleteAsync(""));
            Assert.Equal("User is not valid", exception.Message);
        }

        [Fact]
        public void Authenticate_SendingEmptyValues()
        {
            var exception = Assert.Throws<Exception>(() => _userService.Authenticate(new UserAuthRequestViewModel()));
            Assert.Equal("Email/Password are required.", exception.Message);
        }

        #endregion

        #region ValidatingCorrectObject

        [Fact]
        public async Task Create_SendingValidObject()
        {
            var result = await _userService.CreateAsync(new CreateUserRequestViewModel { Name = "Admin", Email = "admin@gmail.com", Password = "123456" });
            Assert.True(result.Success);
        }

        [Fact]
        public async Task GetAll_ValidatingObject()
        {
            List<User> users = new()
            {
                new User { Id = Guid.NewGuid(), Name = "Usuario Teste", Email = "usuario.teste@gmail.com", CreatedDate = DateTime.Now }
            };

            var userRepository = new Mock<IUserRepository>();
            Func<IQueryable<User>, object>? includes = null;
            userRepository.Setup(x => x.GetAllAsync(includes)).ReturnsAsync(users);

            _userService = new UserService(new Mock<ILogger<UserService>>().Object, userRepository.Object, _mapper);

            var result = await _userService.GetAllAsync();

            Assert.True(result.Datas.Count > 0);
        }

        #endregion

        #region ValidatingRequiredFields

        [Fact]
        public async Task Update_SendingInvalidObject()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _userService.UpdateAsync("", new UpdateUserRequestViewModel()));
            Assert.Equal("The Name field is required.", exception.Message);
        }

        [Fact]
        public async Task Create_SendingInvalidObject()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _userService.CreateAsync(new CreateUserRequestViewModel { Name = "Nicolas Fontes" }));
            Assert.Equal("The Email field is required.", exception.Message);
        }

        #endregion
    }
}
