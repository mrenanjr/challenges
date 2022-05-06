using Microsoft.Extensions.DependencyInjection;
using TemplateS.Application.Interfaces;
using TemplateS.Application.Services;
using TemplateS.Domain.Interfaces;
using TemplateS.Infra.Data.Repositories;

namespace TemplateS.Infra.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IPersonService, PersonService>();

            #endregion

            #region Repositories

            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            #endregion
        }
    }
}