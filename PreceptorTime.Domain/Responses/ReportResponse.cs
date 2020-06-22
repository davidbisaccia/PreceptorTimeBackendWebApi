using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Response
{
    public class ReportResponse
    {
        public string Preceptor { get; set; }
        public string Learner { get; set; }
        public string Rotation { get; set; }
        public int TotalHours { get; set; }
    }
}
