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
    public class ContactService : BaseService<ContactService>, IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public ContactService(ILogger<ContactService> logger, IContactRepository cityRepository, IPersonRepository personRepository, IMapper mapper) : base(logger)
        {
            _contactRepository = cityRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<GetAllResponse<ContactViewModel>> GetAllAsync()
        {
            var contacts = await _contactRepository.GetAllAsync(i => i.Include(x => x.Person));
            var result = new List<ContactViewModel>();

            contacts.ForEach(f =>
            {
                var view = _mapper.Map<ContactViewModel>(f);
                view.Name = f.Person.Name;
                result.Add(view);
            });

            return new GetAllResponse<ContactViewModel>() { Datas = result };
        }

        public async Task<GetResponse<ContactViewModel>> GetByIdAsync(string id)
        {
            var response = new GetResponse<ContactViewModel>();
            var guid = ValidationService.ValidGuid<Contact>(id);
            var contact = await _contactRepository.FindAsync(x => x.Id == guid, x => x.Include(i => i.Person));

            if (contact != null)
            {
                var result = _mapper.Map<ContactViewModel>(contact);
                result.Name = contact.Person.Name;
                result.PersonId = contact.PersonId;
                response.Data = result;
            }

            return response;
        }

        public async Task<CreateResponse<ContactViewModel>> CreateAsync(CreateContactRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);

            var guid = ValidationService.ValidGuid<Person>(viewModel.PersonId);
            var person = _personRepository.Find(x => x.Id == guid);
            ValidationService.ValidExists(person);
            ValidationService.ValidCreateContactRequestObject(viewModel);

            var contact = _mapper.Map<Contact>(viewModel);
            var newContact = await _contactRepository.CreateAsync(contact);
            var contactResult = _mapper.Map<ContactViewModel>(newContact);
            contactResult.Name = person.Name;

            return new CreateResponse<ContactViewModel>() { Data = contactResult };
        }

        public async Task<UpdateResponse> UpdateAsync(string id, UpdateContactRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);

            var guid = ValidationService.ValidGuid<Contact>(id);
            var contact = _contactRepository.Find(x => x.Id == guid);

            ValidationService.ValidExists(contact);
            ValidationService.ValidUpdateContactRequestObject(viewModel);

            _mapper.Map(viewModel, contact);

            await _contactRepository.UpdateAsync(contact);

            return new UpdateResponse();
        }

        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var guid = ValidationService.ValidGuid<Contact>(id);
            var contact = _contactRepository.Find(x => x.Id == guid);

            ValidationService.ValidExists(contact);

            await _contactRepository.DeleteAsync(contact);

            return new DeleteResponse();
        }

        public async Task<DeleteResponse> DeleteAllAsync()
        {
            await _contactRepository.DeleteAllAsync();

            return new DeleteResponse();
        }
    }
}
