//using Microsoft.EntityFrameworkCore;
//using PreceptorTime.Domain.Entities;
//using PreceptorTime.Domain.Repositories;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace PreceptorTime.Infrastructure
//{
//    public class UserContext : DbContext, IUnitOfWork
//    {
//        public DbSet<User> Users { get; set; }
//        //public DbSet<TimeEntry> TimeEntries { get; set; }

//        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            //modelBuilder.ApplyConfiguration(new ItemEntitySchemaDefinition());
//        }

//        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
//        {
//            await SaveChangesAsync(cancellationToken);
//            return true;
//        }
//    }
//}
