using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Requests
{
    public class LogOutRequest
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }
}
