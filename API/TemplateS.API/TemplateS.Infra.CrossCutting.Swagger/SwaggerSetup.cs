using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TemplateS.Infra.CrossCutting.Swagger
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            return services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Desafios Software",
                    Version = "v1",
                    Description = "Desafio técnicio de Software com .NET Core 6 e Frontend",
                    Contact = new OpenApiContact
                    {
                        Name = "Manoel Renan Oliveira Júnior",
                        Email = "mrenanjr@gmail.com"
                    }
                });
            });
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            return app.UseSwagger().UseSwaggerUI(c =>
            {
                c.RoutePrefix = "documentation";
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "API v1");
            });
        }
    }
}