using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Infra.CrossCutting.ExceptionHandler.ViewModels;
using TemplateS.Infra.CrossCutting.ExceptionHandler.Extensions;

namespace TemplateS.Infra.CrossCutting.ExceptionHandler.Middleware
{
    public static class ExceptionHandlerMiddleware
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = async context =>
                {
                    var exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();
                    
                    if (exceptionHandler == null)
                        return;

                    var statusCode = exceptionHandler.Error is ApiException ? ((ApiException)exceptionHandler.Error).StatusCode : HttpStatusCode.InternalServerError;

                    context.Response.StatusCode = (int)statusCode;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(new ExceptionViewModel { Error = exceptionHandler.Error.Message, StatusCode = statusCode }.ToString());
                }
            });
        }
    }
}
