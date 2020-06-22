using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Requests
{
    public class ResetPasswordRequest
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
}
