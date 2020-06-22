using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Responses
{
    public class UserInfoResponse
    {
        public string DisplayName { get; set; }
        public int Id { get; set; }
        public string AccountType { get; set; }
        public bool Active { get; set; }
    }
}
