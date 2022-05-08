using Microsoft.EntityFrameworkCore;
using TemplateS.Application.AutoMapper;
using TemplateS.Infra.CrossCutting.ExceptionHandler.Middleware;
using TemplateS.Infra.CrossCutting.IoC;
using TemplateS.Infra.CrossCutting.Swagger;
using TemplateS.Infra.Data.Context;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddCors();

services.AddEndpointsApiExplorer();
services.AddDbContext<ContextCore>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
services.AddAutoMapper(typeof(AutoMapperSetup));

NativeInjector.RegisterServices(services);
services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseCors(builder =>
{
    builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
});

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ContextCore>();
    
    if(dataContext.Database.GetPendingMigrations().Any())
        dataContext.Database.Migrate();
}

app.UseExceptionHandlerMiddleware();
app.UseSwaggerConfiguration();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
