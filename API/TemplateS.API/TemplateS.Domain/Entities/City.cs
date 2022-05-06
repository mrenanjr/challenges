using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Domain.Common;

namespace TemplateS.Domain.Entities
{
    public sealed class City : BaseEntity
    {
        public string Name { get; set; }
        public string Uf { get; set; }

        public ICollection<Person> Persons { get; set; }
    }
}
