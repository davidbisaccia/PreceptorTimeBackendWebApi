using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreceptorTimeApi.DTO
{
    public class ReportDto
    {
        public string Preceptor { get; set; }
        public string Learner { get; set; }
        public string Rotation { get; set; }
        public int TotalHours { get; set; }
    }
}
