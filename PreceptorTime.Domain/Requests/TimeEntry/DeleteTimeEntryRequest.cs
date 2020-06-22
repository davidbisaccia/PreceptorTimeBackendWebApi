using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Requests
{
    public class DeleteTimeEntryRequest
    {
        public int Id { get; set; }
        public int PreceptorId { get; set; }
        public int StudentId { get; set; }
        public string Rotation { get; set; }
        public int Hours { get; set; }
        public string Date { get; set; }
        public string Notes { get; set; }
    }
}
