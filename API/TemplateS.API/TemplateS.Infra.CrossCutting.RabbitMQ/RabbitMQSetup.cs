using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateS.Infra.CrossCutting.RabbitMQ.Options;

namespace TemplateS.Infra.CrossCutting.RabbitMQ
{
    public static class RabbitMQSetup
    {
        public static IServiceCollection AddRabbitMQConfiguration(this IServiceCollection services, IConfiguration rabbitMQconfig)
        {
            return services.Configure<RabbitMqConfiguration>(rabbitMQconfig);
        }
    }
}