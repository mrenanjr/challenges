using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Application.ViewModels
{
    public class PersonViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Cpf { get; set; }
        public CityViewModel City { get; set; }
        public ContactViewModel Contact { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
