using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Application.ViewModels.Request
{
    public class CreateCityRequestViewModel
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }
        [Required, MaxLength(2)]
        public string Uf { get; set; }
    }
}
