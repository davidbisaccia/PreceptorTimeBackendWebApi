using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Mapper
{
    public interface IReportMapper
    {
        ReportResponse Map(TimeEntry time);
    }
}
