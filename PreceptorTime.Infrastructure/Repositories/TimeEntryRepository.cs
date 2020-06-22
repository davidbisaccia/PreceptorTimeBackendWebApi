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
            return  _context.TimeEntries
                .Add(time)
                .Entity;
        }

        public async Task<bool> Delete(TimeEntry time)
        {
            var found = await GetEntryAsync(time.Id);
            _context.Entry(found).State = EntityState.Deleted;
            return true;
        }

        public async Task<IEnumerable<TimeEntry>> GetYearAsync(int year)
        {
            return await _context.TimeEntries
                .Where(t => t.Date.Year == year)
                .Include(x => x.Student)
                .Include(x => x.Teacher)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeEntry>> GetLearnerAsync(int year, int id)
        {
            return await _context.TimeEntries
                .Where(t => t.Date.Year == year && t.StudentId == id)
                .Include(x => x.Student)
                .Include(x => x.Teacher)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeEntry>> GetTeacherAsync(int year, int id)
        {
            return await _context.TimeEntries
                .Where(t => t.Date.Year == year && t.TeacherId == id)
                .Include(x => x.Student)
                .Include(x => x.Teacher)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetAvailableYears()
        {
            return await _context.TimeEntries
                .Select(x => x.Date.Year)
                .Distinct()
                .ToListAsync();
        }

        public async Task<TimeEntry> Update(TimeEntry time)
        {
            _context.Entry(time).State = EntityState.Modified;
            return time;
        }

        public async Task<TimeEntry> GetEntryAsync(int id)
        {
            var entry = await _context.TimeEntries
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.Student)
                .Include(x => x.Teacher)
                .FirstOrDefaultAsync();

            if (entry == null)
                return null;

            _context.Entry(entry).State = EntityState.Detached;
            return entry;
        }
    }
}
