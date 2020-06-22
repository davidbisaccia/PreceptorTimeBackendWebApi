using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Response;
using PreceptorTime.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreceptorTime.Domain.Services
{
    public interface ITimeEntryService
    {
        Task<IEnumerable<TimeEntryResponse>> GetPreceptorTimeEntriesAsync(PreceptorTimeEntryRequest req);
        Task<IEnumerable<TimeEntryResponse>> GetLearnerTimeEntriesAsync(LearnerTimeEntryRequest req);
        Task<TimeEntryResponse> GetTimeEntryAsync(GetTimeEntryRequest req);
        Task<bool> DeleteTimeEntryAsync(DeleteTimeEntryRequest reqs);
        Task<TimeEntryResponse> AddTimeEntryAsync(AddTimeEntryRequest req);
        Task<TimeEntryResponse> EditTimeEntryAsync(EditTimeEntryRequest req);
    }
}
