using Microsoft.EntityFrameworkCore;
using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreceptorTime.Infrastructure.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly PreceptorTimeContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public TimeEntryRepository(PreceptorTimeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TimeEntry Add(TimeEntry time)
        {
            return _context.TimeEntries
                .Add(time)
                .Entity;
        }

        public bool Delete(TimeEntry time)
        {
            _context.Entry(time).State = EntityState.Deleted;
            return true;
        }

        public async Task<IEnumerable<TimeEntry>> GetAsync(int year)
        {
            return await _context.TimeEntries
                .Where(t => t.Date.Year == year)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeEntry>> GetLearnerAsync(int year, int id)
        {
            return await _context.TimeEntries
                .Where(t => t.Date.Year == year && t.StudentId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeEntry>> GetTeacherAsync(int year, int id)
        {
            return await _context.TimeEntries
                .Where(t => t.Date.Year == year && t.TeacherId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public TimeEntry Update(TimeEntry time)
        {
            _context.Entry(time).State = EntityState.Modified;
            return time;
        }
    }
}
