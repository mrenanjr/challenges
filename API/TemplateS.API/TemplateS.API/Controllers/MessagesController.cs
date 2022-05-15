using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TemplateS.Application.Interfaces;
using TemplateS.Application.ViewModels.Request;

namespace TemplateS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet, Route("[action]")]
        public IActionResult ConsumeMessage() => Ok(_messageService.GetMessage());

        [HttpPost]
        public IActionResult Post(CreateMessageRequestViewModel viewModel) => Ok(_messageService.AddMessage(viewModel));
    }
}
