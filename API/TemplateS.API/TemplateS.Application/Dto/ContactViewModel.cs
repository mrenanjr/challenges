using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Domain.Entities;

namespace TemplateS.Application.ViewModels
{
    public class ContactViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Whatsapp { get; set; }
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
