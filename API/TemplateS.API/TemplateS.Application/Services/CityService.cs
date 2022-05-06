using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.ViewModels;
using TemplateS.Application.Interfaces;
using TemplateS.Domain.Entities;
using TemplateS.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using TemplateS.Application.ViewModels.Response;
using TemplateS.Application.ViewModels.Request;
using Microsoft.Extensions.Logging;
using TemplateS.Application.Common;

namespace TemplateS.Application.Services
{
    public class CityService : BaseService<CityService>, ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(ILogger<CityService> logger, ICityRepository cityRepository, IMapper mapper) : base(logger)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<GetAllResponse<CityViewModel>> GetAllAsync()
        {
            var cities = await _cityRepository.GetAllAsync();

            return new GetAllResponse<CityViewModel>() {
                Datas = cities != null ? cities.Select(a => _mapper.Map<CityViewModel>(a)).ToList() : new List<CityViewModel>()
            };
        }

        public async Task<GetResponse<CityViewModel>> GetByIdAsync(string id)
        {
            var response = new GetResponse<CityViewModel>();
            var guid = ValidationService.ValidGuid<City>(id);
            var city = await _cityRepository.GetAsync(x => x.Id == guid);

            if (city != null) response.Data = _mapper.Map<CityViewModel>(city);

            return response;
        }

        public async Task<CreateResponse<CityViewModel>> CreateAsync(CreateCityRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);

            var city = _mapper.Map<City>(viewModel);
            var newCity = await _cityRepository.CreateAsync(city);

            return new CreateResponse<CityViewModel>() { Data = _mapper.Map<CityViewModel>(newCity) };
        }

        public async Task<UpdateResponse> UpdateAsync(string id, UpdateCityRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);

            var guid = ValidationService.ValidGuid<City>(id);
            var city = _cityRepository.Find(x => x.Id == guid);

            ValidationService.ValidExists(city);

            _mapper.Map(viewModel, city);

            await _cityRepository.UpdateAsync(city);

            return new UpdateResponse();
        }

        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var guid = ValidationService.ValidGuid<City>(id);
            var city = _cityRepository.Find(x => x.Id == guid);

            ValidationService.ValidExists(city);

            await _cityRepository.DeleteAsync(city);

            return new DeleteResponse();
        }
    }
}
