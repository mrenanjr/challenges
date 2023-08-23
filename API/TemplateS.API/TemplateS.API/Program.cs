using Microsoft.EntityFrameworkCore;
using TemplateS.Application.AutoMapper;
using TemplateS.Infra.CrossCutting.Auth;
using TemplateS.Infra.CrossCutting.ExceptionHandler.Middleware;
using TemplateS.Infra.CrossCutting.IoC;
using TemplateS.Infra.CrossCutting.RabbitMQ;
using TemplateS.Infra.CrossCutting.FluentValidation;
using TemplateS.Infra.CrossCutting.Swagger;
using TemplateS.Infra.Data.Context;

var builder = WebApplication.CreateBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if(!string.IsNullOrEmpty(env))
{
    builder.Configuration.AddJsonFile($"appsettings.{env}.json",
            optional: true,
            reloadOnChange: true);
}

var services = builder.Services;
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

services.AddControllers();
services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});
services.AddEndpointsApiExplorer();

var server = builder.Configuration.GetValue<string>("DBServer");
var port = builder.Configuration.GetValue<string>("DBPort");
var user = builder.Configuration.GetValue<string>("DBUser");
var password = builder.Configuration.GetValue<string>("DBPassword");
var database = builder.Configuration.GetValue<string>("Database");
var connectionString = builder.Configuration.GetConnectionString("DBConnection") ?? $"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}";

Console.WriteLine("******* CONNECTION STRING ******** CONNECTION STRING ******** : " + connectionString);

services.AddDbContext<ContextCore>(o => o.UseSqlServer(connectionString));
services.AddAutoMapper(typeof(AutoMapperSetup));
services.AddRabbitMQConfiguration(builder.Configuration.GetSection("RabbitMqConfiguration"));
services.AddFluentValidationSetup();

NativeInjector.RegisterServices(services);
services.AddSwaggerConfiguration();
services.AddAuthConfiguration();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ContextCore>();

    if (dataContext.Database.GetPendingMigrations().Any())
        dataContext.Database.Migrate();
}

app.UseCors(myAllowSpecificOrigins);

app.UseExceptionHandlerMiddleware();
app.UseSwaggerConfiguration();

app.UseRouting();

app.UseAuthConfiguration();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
