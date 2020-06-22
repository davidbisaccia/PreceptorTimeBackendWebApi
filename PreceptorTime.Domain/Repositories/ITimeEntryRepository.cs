using PreceptorTime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreceptorTime.Domain.Repositories
{
    public interface ITimeEntryRepository : IRepository
    {
        Task<IEnumerable<TimeEntry>> GetYearAsync(int year);
        Task<IEnumerable<TimeEntry>> GetLearnerAsync(int year, int id);
        Task<IEnumerable<TimeEntry>> GetTeacherAsync(int year, int id);
        Task<TimeEntry> GetEntryAsync(int id);
        Task<IEnumerable<int>> GetAvailableYears();
        TimeEntry Add(TimeEntry time);
        Task<TimeEntry> Update(TimeEntry time);
        Task<bool> Delete(TimeEntry time);
    }
}
