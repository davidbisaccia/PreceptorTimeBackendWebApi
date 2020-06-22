using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Requests
{
    public class LogInRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
