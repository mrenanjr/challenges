using Microsoft.Extensions.DependencyInjection;
using TemplateS.Application.Interfaces;
using TemplateS.Application.Services;
using TemplateS.Domain.Interfaces;
using TemplateS.Infra.CrossCutting.RabbitMQ.Repositories;
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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPullRequestService, PullRequestService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IBalancedBracketService, BalancedBracketService>();
            services.AddScoped<IContactService, ContactService>();

            #endregion

            #region Repositories

            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPullRequestRepository, PullRequestRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

            #endregion
        }
    }
}