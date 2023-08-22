using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.Dto.Request;

namespace TemplateS.Application.Validations
{
    public class CreateCityRequestValidator : AbstractValidator<CreateCityRequestDto>
    {
        public CreateCityRequestValidator()
        {
            RuleFor(city => city.Name).NotEmpty().MaximumLength(255);
            RuleFor(city => city.Uf).NotEmpty().MaximumLength(2);
        }
    }
}
