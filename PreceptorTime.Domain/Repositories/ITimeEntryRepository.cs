using PreceptorTime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreceptorTime.Domain.Repositories
{
    public interface ITimeEntryRepository : IRepository
    {
        Task<IEnumerable<TimeEntry>> GetAsync(int year);
        Task<IEnumerable<TimeEntry>> GetLearnerAsync(int year, int id);
        Task<IEnumerable<TimeEntry>> GetTeacherAsync(int year, int id);
        TimeEntry Add(TimeEntry time);
        TimeEntry Update(TimeEntry time);
        bool Delete(TimeEntry time);
    }
}
