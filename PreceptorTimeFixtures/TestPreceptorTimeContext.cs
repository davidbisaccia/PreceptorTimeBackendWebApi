using Microsoft.EntityFrameworkCore;
using PreceptorTime.Domain.Entities;
using PreceptorTime.Fixtures.Extensions;
using PreceptorTime.Infrastructure;

namespace PreceptorTime.Fixtures
{
    public class TestPreceptorTimeContext : PreceptorTimeContext
    {
        public TestPreceptorTimeContext(DbContextOptions<PreceptorTimeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed<TimeEntry>("./Data/timeEntry.json");
            modelBuilder.Seed<User>("./Data/user.json");
        }
    }
}
