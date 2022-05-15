using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.Interfaces;
using TemplateS.Application.ViewModels.Response;

namespace TemplateS.Application.Services
{
    public class BalancedBracketService : IBalancedBracketService
    {
        public BalancedBracketService() { }

        public VerifyResponse Verify(string balancedBracket) => new VerifyResponse() { Message = ValidationService.ValidBalancedBracket(balancedBracket) };
    }
}
