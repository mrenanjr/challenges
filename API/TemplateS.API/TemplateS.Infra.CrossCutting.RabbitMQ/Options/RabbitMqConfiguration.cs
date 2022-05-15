using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Infra.CrossCutting.RabbitMQ.Options
{
    public class RabbitMqConfiguration
    {
        public string Host { get; set; }
        public string Queue { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
