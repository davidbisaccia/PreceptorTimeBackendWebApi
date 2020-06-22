using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Responses
{
    public class UserResponse
    {
        public string Email { get; set; }
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string AccountType { get; set; }
        public string Token { get; set; }
        public string TokenExpirationDate { get; set; }
    }
}
