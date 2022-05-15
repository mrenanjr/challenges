using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Domain.Interfaces
{
    public interface IRepositoryMessage<TEntity> : IDisposable where TEntity : class
    {
        TEntity GetMessage();
        TEntity AddMessage(TEntity viewModel);
    }
}
