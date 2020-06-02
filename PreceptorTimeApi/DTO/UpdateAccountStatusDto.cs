using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreceptorTimeApi.DTO
{
    public class UpdateAccountStatusDto
    {
        public int Id { get; set; }
        public bool Active { get; set; }
    }
}
