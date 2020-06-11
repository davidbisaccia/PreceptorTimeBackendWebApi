using Microsoft.EntityFrameworkCore;
using PreceptorTime.Domain.Entities;
using PreceptorTime.Infrastructure.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Infrastructure.Tests
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
