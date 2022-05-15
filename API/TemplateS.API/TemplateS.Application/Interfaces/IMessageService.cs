using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.ViewModels;
using TemplateS.Application.ViewModels.Request;
using TemplateS.Application.ViewModels.Response;

namespace TemplateS.Application.Interfaces
{
    public interface IMessageService
    {
        GetResponse<MessageViewModel> GetMessage();
        CreateResponse<MessageViewModel> AddMessage(CreateMessageRequestViewModel viewModel);
    }
}
