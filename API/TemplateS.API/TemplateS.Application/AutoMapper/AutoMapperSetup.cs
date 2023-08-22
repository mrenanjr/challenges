using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.Dto.Request;
using TemplateS.Application.ViewModels;
using TemplateS.Application.ViewModels.Request;
using TemplateS.Domain.Entities;

namespace TemplateS.Application.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            #region Primitive types

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<string?, Guid>().ConvertUsing((src, dest) => src != null ? new Guid(src) : dest);
            CreateMap<string?, string>().ConvertUsing((src, dest) => src ?? dest);

            #endregion

            #region ViewModelToViewModel

            CreateMap<CreatePersonRequestViewModel, CreateContactRequestViewModel>();
            CreateMap<UpdatePersonRequestViewModel, CreateContactRequestViewModel>();
            CreateMap<PersonViewModel, ContactViewModel>();
            
            #endregion

            #region DtoToDomain

            CreateMap<CityViewModel, City>();
            CreateMap<CreateCityRequestDto, City>();
            CreateMap<UpdateCityRequestDto, City>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PersonViewModel, Person>();
            CreateMap<CreatePersonRequestViewModel, Person>();
            CreateMap<UpdatePersonRequestViewModel, Person>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdatePersonRequestViewModel, Contact>();
            CreateMap<UserViewModel, User>();
            CreateMap<CreateUserRequestViewModel, User>();
            CreateMap<UpdateUserRequestViewModel, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PullRequestViewModel, PullRequest>();
            CreateMap<CreatePullRequestRequestViewModel, PullRequest>();
            CreateMap<UpdatePullRequestRequestViewModel, PullRequest>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<MessageViewModel, Message>();
            CreateMap<CreateMessageRequestViewModel, Message>();
            CreateMap<CreateContactRequestViewModel, Contact>();
            CreateMap<UpdateContactRequestViewModel, Contact>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region DomainToViewModel

            CreateMap<City, CityViewModel>();
            CreateMap<Person, PersonViewModel>();
            CreateMap<PullRequest, PullRequestViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<Message, MessageViewModel>();
            CreateMap<Contact, ContactViewModel>();

            #endregion
        }
    }
}
