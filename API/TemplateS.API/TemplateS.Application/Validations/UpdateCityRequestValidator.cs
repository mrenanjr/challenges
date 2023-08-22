using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.Dto.Request;
using TemplateS.Application.ViewModels.Request;

namespace TemplateS.Application.Validations
{
    public class UpdateCityRequestValidator : AbstractValidator<UpdateCityRequestDto>
    {
        public UpdateCityRequestValidator()
        {
            RuleFor(city => city.Name).MaximumLength(200);
            RuleFor(city => city.Uf).MaximumLength(2);
        }
    }
}
