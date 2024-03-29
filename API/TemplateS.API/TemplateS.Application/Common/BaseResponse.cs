﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateS.Application.Common
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; } = true;

        public object? Error { get; set; } = null;
    }
}
