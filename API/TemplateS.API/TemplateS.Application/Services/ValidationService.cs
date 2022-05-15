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
        #region Generic Validators

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

        public static void ValidUpdatePersonRequestObject(UpdatePersonRequestViewModel obj)
        {
            if (obj.Age.HasValue && obj.Age.Value <= 0)
                NotValid(nameof(obj.Age));

            if(obj.Cpf != null && (obj.Cpf.Length != 11 || !IsDigitsOnly(obj.Cpf)))
                NotValid(nameof(obj.Cpf));
        }

        public static void ValidCreatePersonRequestObject(CreatePersonRequestViewModel obj)
        {
            if (obj.Age <= 0)
                NotValid(nameof(obj.Age));

            if (obj.Cpf != null && (obj.Cpf.Length != 11 || !IsDigitsOnly(obj.Cpf)))
                NotValid(nameof(obj.Cpf));
        }

        #endregion

        #region Balanced Brackets Validators

        public static string ValidBalancedBracket(string balancedBracket)
        {
            var property = "Balanced Bracket";
            var matches = Regex.Matches(balancedBracket, @"{|}|\(|\)|\[|\]");

            if (balancedBracket.Length % 2 != 0 || matches.Count != balancedBracket.Length)
                NotValid(property);

            var result = Validate(balancedBracket);
            
            return result ? $"{property} is valid." : $"{property} is not valid.";
        }

        private static bool Validate(string balancedBracket)
        {
            Stack<char> stack = new();
            List<char> openBrackets = new() { '(', '[', '{' };
            List<char> closedBrackets = new() { ')', ']', '}' };

            balancedBracket.ToList().ForEach(bracket =>
            {
                if (stack.Count == 0 || openBrackets.Contains(bracket))
                {
                    stack.Push(bracket);
                    return;
                }

                stack.TryPeek(out char top);
                if (closedBrackets.Contains(bracket) && top == GetOpposity(bracket))
                    stack.Pop();
            });

            return stack.Count == 0;
        }

        private static char GetOpposity(char bracket)
        {
            Dictionary<char, char> mapper = new()
            {
                { '(', ')' },
                { ')', '(' },
                { '[', ']' },
                { ']', '[' },
                { '{', '}' },
                { '}', '{' }
            };

            return mapper[bracket];
        }

        #endregion

        #region Contacts Validators

        public static void ValidCreateContactRequestObject(CreateContactRequestViewModel obj)
        {
            if (obj.Whatsapp != null && !IsDigitsOnly(obj.Whatsapp))
                NotValid(nameof(obj.Whatsapp));

            if (obj.Phone != null && !IsDigitsOnly(obj.Phone))
                NotValid(nameof(obj.Phone));
        }

        public static void ValidUpdateContactRequestObject(UpdateContactRequestViewModel obj)
        {
            if (obj.Whatsapp != null && !IsDigitsOnly(obj.Whatsapp))
                NotValid(nameof(obj.Whatsapp));

            if (obj.Phone != null && !IsDigitsOnly(obj.Phone))
                NotValid(nameof(obj.Phone));
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
