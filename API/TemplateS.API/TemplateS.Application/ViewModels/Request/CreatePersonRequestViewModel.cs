using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Application.ViewModels.Request
{
    public class CreatePersonRequestViewModel
    {
        [Required, MaxLength(300)]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required, StringLength(11)]
        public string Cpf { get; set; }
        [Required]
        public string CityId { get; set; }
    }
}
