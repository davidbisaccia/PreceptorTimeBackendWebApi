using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Requests
{
    public class UpdateAccountStatusRequest
    {
        public int Id { get; set; }
        public bool Active { get; set; }
    }
}
