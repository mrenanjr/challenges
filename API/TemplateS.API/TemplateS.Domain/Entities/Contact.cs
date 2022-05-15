using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Domain.Common;

namespace TemplateS.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Whatsapp { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}
