using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.Common;

namespace TemplateS.Application.ViewModels.Response
{
    public class CreateResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
