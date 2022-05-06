using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TemplateS.Application.ViewModels.Request;
using TemplateS.Domain.Entities;
using TemplateS.Infra.CrossCutting.ExceptionHandler.Extensions;

namespace TemplateS.Application.Services
{
    public static class ValidationService
    {
        #region Generic Validations

        public static Guid ValidGuid<T>(string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
                NotValid(typeof(T).Name);

            return guid;
        }

        public static bool ValidExists<T>(T TEntity)
        {
            if (TEntity == null)
                NotExists(typeof(T).Name);

            return true;
        }

        #endregion

        #region Persons Validators

        public static bool ValidUpdatePersonRequestObject(UpdatePersonRequestViewModel obj)
        {
            if (obj.Age.HasValue && obj.Age.Value <= 0)
                NotValid(nameof(obj.Age));

            if(obj.Cpf != null && (obj.Cpf.Length != 11 || !IsDigitsOnly(obj.Cpf)))
                NotValid(nameof(obj.Cpf));

            return true;
        }

        #endregion

        private static void NotValid(string property) => throw new ApiException($"{property} is not valid", HttpStatusCode.BadRequest);
        private static void NotExists(string property) => throw new ApiException($"{property} not exists", HttpStatusCode.BadRequest);
        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
