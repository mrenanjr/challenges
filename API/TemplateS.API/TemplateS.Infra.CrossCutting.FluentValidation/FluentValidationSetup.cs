using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace TemplateS.Infra.CrossCutting.FluentValidation
{
    public static class FluentValidationSetup
    {
        public static IServiceCollection AddFluentValidationSetup(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            return services;
        }

        //public static List<Api>? GetErrors(this ValidationResult result)
        //{
        //    return result.Errors?.Select(error => new MessageResult(error.error.PropertyName, error.error.ErrorMessage)).ToList();
        //}
    }
}