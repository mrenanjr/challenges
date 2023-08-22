using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Application.ViewModels
{
    public class PullRequestViewModel
    {
        public string Id { get; set; }
        public string Githubid { get; set; }
        public string Title { get; set; }
        public string Login { get; set; }
        public string State { get; set; }
        public string Url { get; set; }
    }
}
