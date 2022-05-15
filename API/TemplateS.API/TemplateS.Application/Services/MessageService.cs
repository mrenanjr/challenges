using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.Common;
using TemplateS.Application.Interfaces;
using TemplateS.Application.ViewModels;
using TemplateS.Application.ViewModels.Request;
using TemplateS.Application.ViewModels.Response;
using TemplateS.Domain.Entities;
using TemplateS.Domain.Interfaces;

namespace TemplateS.Application.Services
{
    public class MessageService : BaseService<MessageService>, IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(ILogger<MessageService> logger, IMessageRepository messageRepository, IMapper mapper) : base(logger)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public GetResponse<MessageViewModel> GetMessage()
        {
            var message = _messageRepository.GetMessage();

            return new GetResponse<MessageViewModel>() { Data = _mapper.Map<MessageViewModel>(message) };
        }

        public CreateResponse<MessageViewModel> AddMessage(CreateMessageRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);

            var message = _mapper.Map<Message>(viewModel);
            var messageResponse = _messageRepository.AddMessage(message);

            return new CreateResponse<MessageViewModel>() { Data = _mapper.Map<MessageViewModel>(messageResponse) };
        }
    }
}
