using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.ViewModels.Response;

namespace TemplateS.Application.Interfaces
{
    public interface IBalancedBracketService
    {
        VerifyResponse Verify(string balancedBracket);
    }
}
