using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Mapper
{
    public class ReportMapper : IReportMapper
    {
        public ReportResponse Map(TimeEntry time)
        {
            if (time == null)
                return null;

            return new ReportResponse()
            {
                Learner = time.Student.DisplayName,
                Preceptor = time.Teacher.DisplayName,
                Rotation = time.Rotation,
                TotalHours = time.Hours, //TODO: this is supposed to be a summation....
            };
        }
    }
}
