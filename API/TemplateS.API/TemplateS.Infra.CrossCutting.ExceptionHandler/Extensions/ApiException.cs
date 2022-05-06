using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Infra.CrossCutting.ExceptionHandler.Extensions
{
    public class ApiException : Exception
    {
        public ApiException() { }

        public ApiException(string message,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            Exception? innerException = null
        ) : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public ApiException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public ApiException(string message, Exception innerException) : base(message, innerException) { }

        public HttpStatusCode StatusCode { get; set; }
    }
}
