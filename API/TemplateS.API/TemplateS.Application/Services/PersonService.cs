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
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public PersonService(ILogger<PersonService> logger, IPersonRepository personRepository, ICityRepository cityRepository, IContactRepository contactRepository, IMapper mapper) : base(logger)
        {
            _personRepository = personRepository;
            _cityRepository = cityRepository;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<GetAllResponse<PersonViewModel>> GetAllAsync()
        {
            var persons = await _personRepository.GetAllAsync(i => i.Include(x => x.City).Include(x => x.Contact));

            return new GetAllResponse<PersonViewModel>() { Datas = _mapper.Map<List<PersonViewModel>>(persons) };
        }

        public async Task<GetResponse<PersonViewModel>> GetByIdAsync(string id)
        {
            var guid = ValidationService.ValidGuid<Person>(id);
            var person = await _personRepository.FindAsync(x => x.Id == guid, i => i.Include(x => x.City).Include(x => x.Contact));

            return new GetResponse<PersonViewModel>() { Data = _mapper.Map<PersonViewModel>(person) };
        }

        public async Task<CreateResponse<PersonViewModel>> CreateAsync(CreatePersonRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);
            ValidationService.ValidCreatePersonRequestObject(viewModel);

            var guid = ValidationService.ValidGuid<City>(viewModel.CityId);
            var city = _cityRepository.Find(x => x.Id == guid);
            ValidationService.ValidExists(city);

            var viewModelContact = _mapper.Map<CreateContactRequestViewModel>(viewModel);

            var person = _mapper.Map<Person>(viewModel);
            var personNew = await _personRepository.CreateAsync(person);

            viewModelContact.PersonId = personNew.Id.ToString();

            var contact = await _contactRepository.CreateAsync(_mapper.Map<Contact>(viewModelContact));
            var personResult = _mapper.Map<PersonViewModel>(personNew);
            
            personResult.Contact = _mapper.Map<ContactViewModel>(contact);
            personResult.Contact.Name = personResult.Name;
            personResult.City = _mapper.Map<CityViewModel>(city);

            return new CreateResponse<PersonViewModel>() { Data = personResult };
        }

        public async Task<UpdateResponse> UpdateAsync(string id, UpdatePersonRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);
            ValidationService.ValidUpdatePersonRequestObject(viewModel);

            var guid = ValidationService.ValidGuid<Person>(id);
            var person = await _personRepository.FindAsync(x => x.Id == guid, x => x.Include(i => i.Contact));
            ValidationService.ValidExists(person);

            var contact = person.Contact;

            if(contact == null)
            {
                var viewModelContact = _mapper.Map<CreateContactRequestViewModel>(viewModel);
                viewModelContact.PersonId = person.Id.ToString();

                contact = await _contactRepository.CreateAsync(_mapper.Map<Contact>(viewModelContact));
            }
            else
            {
                _mapper.Map(viewModel, contact);
                await _contactRepository.UpdateAsync(contact);
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

        public async Task<DeleteResponse> DeleteAllAsync()
        {
            await _personRepository.DeleteAllAsync();

            return new DeleteResponse();
        }
    }
}
