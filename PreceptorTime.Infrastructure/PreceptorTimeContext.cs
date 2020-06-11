using Microsoft.EntityFrameworkCore;
using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Repositories;
using PreceptorTime.Infrastructure.SchemaDefinitions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PreceptorTime.Infrastructure
{
    public class PreceptorTimeContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }

        public PreceptorTimeContext(DbContextOptions<PreceptorTimeContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new TimeEntryEntitySchemaDefinition())
                .ApplyConfiguration(new UserEntitySchemaDefinition());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
