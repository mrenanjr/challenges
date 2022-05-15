using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.ViewModels.Request;
using TemplateS.Domain.Entities;
using TemplateS.Domain.Interfaces;
using TemplateS.Infra.CrossCutting.RabbitMQ.Options;

namespace TemplateS.Infra.CrossCutting.RabbitMQ.Repositories
{
    public class MessageRepository : RepositoryMessage<Message>, IMessageRepository
    {   
        public MessageRepository(IOptions<RabbitMqConfiguration> options) : base(options) { }
    }
}
