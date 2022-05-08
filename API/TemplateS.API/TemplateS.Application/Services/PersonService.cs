using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.Common;
using TemplateS.Application.Interfaces;
using TemplateS.Application.ViewModels;
using TemplateS.Application.ViewModels.Request;
using TemplateS.Application.ViewModels.Response;
using TemplateS.Domain.Entities;
using TemplateS.Domain.Interfaces;

namespace TemplateS.Application.Services
{
    public class PersonService : BaseService<PersonService>, IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public PersonService(ILogger<PersonService> logger, IPersonRepository personRepository, ICityRepository cityRepository, IMapper mapper) : base(logger)
        {
            _personRepository = personRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<GetAllResponse<PersonViewModel>> GetAllAsync()
        {
            var persons = await _personRepository.GetAllAsync(i => i.Include(x => x.City));
            var resultList = new List<PersonViewModel>();
            
            persons.ForEach(f =>
            {
                var personVieModel = _mapper.Map<PersonViewModel>(f);
                personVieModel.City = f.City.Name;
                resultList.Add(personVieModel);
            });

            return new GetAllResponse<PersonViewModel>() { Datas = resultList };
        }

        public async Task<GetResponse<PersonViewModel>> GetByIdAsync(string id)
        {
            var response = new GetResponse<PersonViewModel>();
            var guid = ValidationService.ValidGuid<Person>(id);
            var person = await _personRepository.FindAsync(x => x.Id == guid, i => i.Include(x => x.City));

            if (person != null)
            {
                response.Data = _mapper.Map<PersonViewModel>(person);
                response.Data.City = person.City.Name;
            }

            return response;
        }

        public async Task<CreateResponse<PersonViewModel>> CreateAsync(CreatePersonRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);
            var guid = ValidationService.ValidGuid<City>(viewModel.CityId);
            var city = _cityRepository.Find(x => x.Id == guid);
            ValidationService.ValidExists(city);
            ValidationService.ValidCreatePersonRequestObject(viewModel);

            var person = _mapper.Map<Person>(viewModel);
            var personCity = await _personRepository.CreateAsync(person);
            var personResult = _mapper.Map<PersonViewModel>(personCity);
            personResult.City = city.Name;

            return new CreateResponse<PersonViewModel>() { Data = personResult };
        }

        public async Task<UpdateResponse> UpdateAsync(string id, UpdatePersonRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);

            var guid = ValidationService.ValidGuid<Person>(id);
            var person = _personRepository.Find(x => x.Id == guid);

            ValidationService.ValidExists(person);
            ValidationService.ValidUpdatePersonRequestObject(viewModel);
            
            if(viewModel.CityId != null)
            {
                guid = ValidationService.ValidGuid<City>(viewModel.CityId);

                var city = _cityRepository.Find(x => x.Id == guid);
                ValidationService.ValidExists(city);
            }

            _mapper.Map(viewModel, person);

            await _personRepository.UpdateAsync(person);

            return new UpdateResponse();
        }

        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var guid = ValidationService.ValidGuid<Person>(id);
            var person = _personRepository.Find(x => x.Id == guid);

            ValidationService.ValidExists(person);

            await _personRepository.DeleteAsync(person);

            return new DeleteResponse();
        }
    }
}
