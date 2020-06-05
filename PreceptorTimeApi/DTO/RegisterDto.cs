using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreceptorTimeApi.DTO
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string AccountType { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
