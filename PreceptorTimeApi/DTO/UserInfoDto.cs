using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreceptorTime.Api.DTO
{
    public class UserInfoDto
    {
        public string DisplayName { get; set; }
        public int Id { get; set; }
        public string AccountType { get; set; }
        public bool Active { get; set; }
    }
}
