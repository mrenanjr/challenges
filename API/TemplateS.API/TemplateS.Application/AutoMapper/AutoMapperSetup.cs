using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.ViewModels;
using TemplateS.Application.ViewModels.Request;
using TemplateS.Domain.Entities;

namespace TemplateS.Application.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<string?, Guid>().ConvertUsing((src, dest) => src != null ? new Guid(src) : dest);

            #region ViewModelToDomain

            CreateMap<CityViewModel, City>();
            CreateMap<CreateCityRequestViewModel, City>();
            CreateMap<UpdateCityRequestViewModel, City>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PersonViewModel, Person>();
            CreateMap<CreatePersonRequestViewModel, Person>();
            CreateMap<UpdatePersonRequestViewModel, Person>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region DomainToViewModel

            CreateMap<City, CityViewModel>();
            CreateMap<Person, PersonViewModel>();

            #endregion
        }
    }
}
