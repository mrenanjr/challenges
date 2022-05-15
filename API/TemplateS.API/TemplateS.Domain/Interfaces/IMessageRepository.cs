using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Domain.Entities;

namespace TemplateS.Domain.Interfaces
{
    public interface IMessageRepository : IRepositoryMessage<Message>
    {
    }
}
