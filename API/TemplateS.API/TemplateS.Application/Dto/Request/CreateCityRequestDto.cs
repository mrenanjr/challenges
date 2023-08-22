using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.Validations;

namespace TemplateS.Application.Dto.Request
{
    public class CreateCityRequestDto
    {
        public string Name { get; set; }
        public string Uf { get; set; }
    }
}
