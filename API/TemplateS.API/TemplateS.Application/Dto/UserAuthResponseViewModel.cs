using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Application.ViewModels
{
    public class UserAuthResponseViewModel
    {
        public UserAuthResponseViewModel(UserViewModel user, string token)
        {
            this.User = user;
            this.Token = token;
        }

        public UserViewModel User { get; set; }
        public string Token { get; set; }
    }
}
