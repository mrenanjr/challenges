using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Application.Common
{
    public abstract class BaseService<T>
    {
        public readonly ILogger<T> _logger;

        public BaseService(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
