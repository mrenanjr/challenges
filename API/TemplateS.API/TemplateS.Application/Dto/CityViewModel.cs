using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Application.ViewModels
{
    public class CityViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Uf { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
