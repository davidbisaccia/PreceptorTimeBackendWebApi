using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreceptorTime.Api.DTO
{
    public class UpdateAccountStatusDto
    {
        public int Id { get; set; }
        public bool Active { get; set; }
    }
}
