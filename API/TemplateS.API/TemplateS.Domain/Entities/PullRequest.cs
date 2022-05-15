using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Domain.Common;

namespace TemplateS.Domain.Entities
{
    public class PullRequest : BaseEntity
    {
        public string Githubid { get; set; }
        public string Title { get; set; }
        public string Login { get; set; }
        public string State { get; set; }
        public string Url { get; set; }
    }
}
