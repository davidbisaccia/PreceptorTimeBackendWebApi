using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PreceptorTime.Api.DTO
{
    public class TimeEntryDto
    {
        public int Id { get; set; }
        public int PreceptorId { get; set; }
        public string PreceptorDisplayName { get; set; }
        public int StudentId { get; set; }
        public string StudentDisplayName { get; set; }
        public string Rotation { get; set; }
        public int Hours { get; set; }
        public string Date { get; set; }
        public string Notes { get; set; }
    }
}
