using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreceptorTime.Domain.Services
{
    public interface IReportsService
    {
        Task<IEnumerable<ReportResponse>> GetPreceptorReportsAsync(PreceptorReportRequest req);
        Task<IEnumerable<ReportResponse>> GetLearnerReportsAsync(LearnerReportRequest req);
        Task<IEnumerable<ReportResponse>> GetYearReportsAsync(ReportYearRequest req);

        Task<IEnumerable<int>> GetAvilableYears();
    }
}
