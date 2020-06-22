using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Requests
{
    public class RegisterUserRequest
    {
        public string DisplayName { get; set; }
        public string AccountType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
