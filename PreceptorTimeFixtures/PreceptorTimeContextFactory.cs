using Microsoft.EntityFrameworkCore;
using PreceptorTime.Domain.Mapper;
using PreceptorTime.Infrastructure;
using System;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using PreceptorTime.Domain.Entities;
using System.Linq;

namespace PreceptorTime.Fixtures
{
    public class PreceptorTimeContextFactory
    {
        public readonly TestPreceptorTimeContext ContextInstance;
        public readonly IReportMapper ReportMapper;
        public readonly IUserInfoMapper UserInfoMapper;
        public readonly IUserMapper UserMapper;
        public readonly ITimeEntryMapper TimeEntryMapper;

        public PreceptorTimeContextFactory()
        {
            //Notice that we are building ContextOptions using Guid.NewGuid().ToString() property as a database 
            //name in order to provide a new, clean in-memory instance for each test class.
            var contextOptions = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            EnsureCreation(contextOptions);
            ContextInstance = new TestPreceptorTimeContext(contextOptions);

            ReportMapper = new ReportMapper();
            UserInfoMapper = new UserInfoMapper();
            UserMapper = new UserMapper();
            TimeEntryMapper = new TimeEntryMapper(UserMapper);
        }

        private void EnsureCreation(DbContextOptions<PreceptorTimeContext> contextOptions)
        {
            using var context = new TestPreceptorTimeContext(contextOptions);
            context.Database.EnsureCreated();
        }
    }
}
