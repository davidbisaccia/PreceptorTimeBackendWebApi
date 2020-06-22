using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PreceptorTime.Domain.Mapper;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Services;
using PreceptorTime.Fixtures;
using PreceptorTime.Infrastructure.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PreceptorTime.Domain.Tests
{
    public class TimeEntryServiceTests : IClassFixture<PreceptorTimeContextFactory>
    {
        private readonly TimeEntryRepository timeRepo;
        private readonly TimeEntryMapper timeMapper;
        private readonly UserMapper userMapper;

        public TimeEntryServiceTests(PreceptorTimeContextFactory factory)
        {
            timeRepo = new TimeEntryRepository(factory.ContextInstance);
            userMapper = new UserMapper();
            timeMapper = new TimeEntryMapper(userMapper);
        }

        [Theory]
        [InlineData(2, 2020, true)]
        [InlineData(2, 2000, false)]
        [InlineData(1, 2020, false)]
        public async Task GetLearnerTimeEntriesAsync(int id, int year, bool success)
        {
            var req = new LearnerTimeEntryRequest()
            {
                   Id = id,
                   Year = year,
            };
            var service = new TimeEntryService(timeMapper, timeRepo);
            var result = await service.GetLearnerTimeEntriesAsync(req);
            if (success)
            {
                result.ShouldNotBeEmpty();
            }
            else
            {
                result.ShouldBeEmpty();
            }
        }

        [Theory]
        [InlineData(1, 2020, true)]
        [InlineData(1, 2000, false)]
        [InlineData(2, 2020, false)]
        public async Task GetPreceptorTimeEntriesAsync(int id, int year, bool success)
        {
            var req = new PreceptorTimeEntryRequest()
            {
                Id = id,
                Year = year,
            };
            var service = new TimeEntryService(timeMapper, timeRepo);
            var result = await service.GetPreceptorTimeEntriesAsync(req);
            if (success)
            {
                result.ShouldNotBeEmpty();
            }
            else
            {
                result.ShouldBeEmpty();
            }
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(20, false)]
        public async Task GetTimeEntryAsync(int id, bool success)
        {
            var req = new GetTimeEntryRequest()
            {
                Id = id,
            };
            var service = new TimeEntryService(timeMapper, timeRepo);
            var result = await service.GetTimeEntryAsync(req);
            if (success)
            {
                result.ShouldNotBeNull();
            }
            else
            {
                result.ShouldBeNull();
            }
        }

        [Theory]
        [InlineData("{ \"PreceptorId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"AnotherRotation\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task AddTimeEntryAsync(string jsonEntity)
        {
            var req = JsonConvert.DeserializeObject<AddTimeEntryRequest>(jsonEntity);

            var service = new TimeEntryService(timeMapper, timeRepo);
            var result = await service.AddTimeEntryAsync(req);

            result.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("{ \"Id\": 1, \"PreceptorId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"AnotherRotation\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task EditTimeEntryAsync_Success(string jsonEntity)
        {
            var req = JsonConvert.DeserializeObject<EditTimeEntryRequest>(jsonEntity);

            var service = new TimeEntryService(timeMapper, timeRepo);
            var result = await service.EditTimeEntryAsync(req);

            result.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("{ \"Id\": 10, \"PreceptorId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"AnotherRotation\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task EditTimeEntryAsync_Failure_DoesNotExist(string jsonEntity)
        {
            var req = JsonConvert.DeserializeObject<EditTimeEntryRequest>(jsonEntity);

            var service = new TimeEntryService(timeMapper, timeRepo);

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
            {
                var result = await service.EditTimeEntryAsync(req);
            });
        }

        [Theory]
        [InlineData("{ \"Id\": 3, \"PreceptorId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"AnotherRotation\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task DeleteTimeEntryAsync_Success_DoesNotExist(string jsonEntity)
        {
            var req = JsonConvert.DeserializeObject<DeleteTimeEntryRequest>(jsonEntity);

            var service = new TimeEntryService(timeMapper, timeRepo);

            var result = await service.DeleteTimeEntryAsync(req);
            result.ShouldBeTrue();
        }

        [Theory]
        [InlineData("{ \"Id\": 10, \"PreceptorId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"AnotherRotation\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task DeleteTimeEntryAsync_Failure_DoesNotExist(string jsonEntity)
        {
            var req = JsonConvert.DeserializeObject<DeleteTimeEntryRequest>(jsonEntity);

            var service = new TimeEntryService(timeMapper, timeRepo);

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
            {
                var result = await service.DeleteTimeEntryAsync(req);
            });
        }
    }
}
