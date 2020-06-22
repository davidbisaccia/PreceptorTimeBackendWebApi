using PreceptorTime.Domain.Mapper;
using PreceptorTime.Domain.Repositories;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreceptorTime.Domain.Services
{
    public class ReportsService : IReportsService
    {
        private readonly ITimeEntryRepository _repo;
        private readonly IReportMapper _mapper;

        public ReportsService(ITimeEntryRepository repo, IReportMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReportResponse>> GetYearReportsAsync(ReportYearRequest req)
        {
            var results = await _repo.GetYearAsync(req.Year);
            return results.Select(x => _mapper.Map(x)).ToList();
        }

        public async Task<IEnumerable<ReportResponse>> GetLearnerReportsAsync(LearnerReportRequest req)
        {
            var results = await _repo.GetLearnerAsync(req.Year, req.Id);
            return results.Select(x => _mapper.Map(x)).ToList();
        }

        public async Task<IEnumerable<ReportResponse>> GetPreceptorReportsAsync(PreceptorReportRequest req)
        {
            var results = await _repo.GetTeacherAsync(req.Year, req.Id);
            return results.Select(x => _mapper.Map(x)).ToList();
        }

        public async Task<IEnumerable<int>> GetAvilableYears()
        {
            var result = await _repo.GetAvailableYears();
            return result.ToList();
        }
    }
}
