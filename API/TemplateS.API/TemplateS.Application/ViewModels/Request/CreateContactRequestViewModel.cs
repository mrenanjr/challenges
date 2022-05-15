using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Application.ViewModels.Request
{
    public class CreateContactRequestViewModel
    {
        [Required]
        public string PersonId { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string? Phone { get; set; }
        [MaxLength(20)]
        public string? Whatsapp { get; set; }
    }
}
