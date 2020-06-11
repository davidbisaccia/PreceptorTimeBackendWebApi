using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Entities
{
    public class TimeEntry
    {
        //time id, teacher id, student id, hours, date, rotation name, notes
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public User Teacher { get; set; }
        public int StudentId { get; set; }
        public User Student { get; set; }
        public int Hours { get; set; }
        public DateTime Date { get; set; }
        public string Rotation { get; set; }
        public string Notes { get; set; }
    }
}
