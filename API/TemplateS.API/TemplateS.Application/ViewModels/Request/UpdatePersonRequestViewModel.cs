using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.Common;

namespace TemplateS.Application.ViewModels.Request
{
    public class UpdatePersonRequestViewModel
    {
        [MaxLength(300)]
        public string? Name { get; set; }
        public int? Age { get; set; }
        [StringLength(11)]
        public string? Cpf { get; set; }
        public string? CityId { get; set; }
    }
}
