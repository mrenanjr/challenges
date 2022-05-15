using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Application.ViewModels
{
    public class MessageViewModel
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
