using Microsoft.EntityFrameworkCore;
using PreceptorTime.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Newtonsoft.Json;
using PreceptorTime.Domain.Entities;
using System.Linq;
using PreceptorTime.Fixtures;

namespace PreceptorTime.Infrastructure.Tests
{
    public class TimeEntryRepositoryTests : IClassFixture<PreceptorTimeContextFactory>
    {
        private readonly TimeEntryRepository _sut;
        private readonly TestPreceptorTimeContext _context;

        public TimeEntryRepositoryTests(PreceptorTimeContextFactory factory)
        {
            _context = factory.ContextInstance;
            _sut = new TimeEntryRepository(_context);
        }

        [Fact]
        public async Task GetAsync_Year2020_Success()
        {
            //var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
            //    .UseInMemoryDatabase(databaseName: "should_get_data")
            //    .Options;

            //await using var context = new TestPreceptorTimeContext(options);
            //context.Database.EnsureCreated();

            //var sut = new TimeEntryRepository(context);
            var result = await _sut.GetYearAsync(2020);
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Year2021_Empty()
        {
            var result = await _sut.GetYearAsync(2021);
            result.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetLearnerAsync_Id1_Year2020_Empty()
        {
            var result = await _sut.GetLearnerAsync(2020, 1);
            result.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetLearnerAsync_Id2_Year2021_Empty()
        {
            var result = await _sut.GetLearnerAsync(2021, 2);
            result.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetLearnerAsync_Id2_Year2020_Success()
        {
            var result = await _sut.GetLearnerAsync(2020, 2);
            result.ShouldNotBeEmpty();
        }


        [Fact]
        public async Task GetTeacherAsync_Id2_Year2020_Empty()
        {
            var result = await _sut.GetTeacherAsync(2020, 2);
            result.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetTeacherAsync_Id1_Year2021_Empty()
        {
            var result = await _sut.GetTeacherAsync(2021, 1);
            result.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetTeacherAsync_Id1_Year2020_Success()
        {
            var result = await _sut.GetTeacherAsync(2020, 1);
            result.ShouldNotBeEmpty();
        }

        [Theory]
        [InlineData("{ \"Id\" : 1, \"TeacherId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"AnotherRotation\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task Add_NewTimeEntry_Failure(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<TimeEntry>(jsonEntity);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                _sut.Add(entity);
                await _sut.UnitOfWork.SaveEntitiesAsync();
            });
        }

        [Theory]
        [InlineData("{ \"Id\" : 7, \"TeacherId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"AnotherRotation\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task Add_NewTimeEntry_Success(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<TimeEntry>(jsonEntity);
            _sut.Add(entity);

            await _sut.UnitOfWork.SaveEntitiesAsync();

            _context.TimeEntries
                .FirstOrDefault(x => x.Id == entity.Id)
                .ShouldNotBeNull();
        }

        [Theory]
        [InlineData("{ \"Id\" : 7, \"TeacherId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"AnotherRotation\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task Update_TimeEntry_Throws(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<TimeEntry>(jsonEntity);

            await Assert.ThrowsAsync<InvalidOperationException>( async () =>
            {
                _sut.Update(entity);
                await _sut.UnitOfWork.SaveEntitiesAsync();
            });
        }

        [Theory]
        [InlineData("{ \"Id\" : 6,	\"TeacherId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"Rhuem\", \"Hours\" : 450, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task Update_TimeEntry_Success(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<TimeEntry>(jsonEntity);

            _sut.Update(entity);
            await _sut.UnitOfWork.SaveEntitiesAsync();

            _context.TimeEntries
                .FirstOrDefault(x => x.Id == entity.Id)
                ?.Hours.ShouldBe(450);
        }

        [Theory]
        [InlineData("{ \"Id\" : 6,	\"TeacherId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"Rhuem\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task Delete_TimeEntry_Success(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<TimeEntry>(jsonEntity);

            await _sut.Delete(entity);
            await _sut.UnitOfWork.SaveEntitiesAsync();

            _context.TimeEntries
                .FirstOrDefault(x => x.Id == entity.Id)
                .ShouldBeNull();
        }

        [Theory]
        [InlineData("{ \"Id\" : 7,	\"TeacherId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"Rhuem\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task Delete_TimeEntry_Failure(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<TimeEntry>(jsonEntity);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _sut.Delete(entity);
                await _sut.UnitOfWork.SaveEntitiesAsync();
            });
        }

        [Fact]
        public async Task GetAsync_Id1_Success()
        {
            var result = await _sut.GetEntryAsync(1);
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Id10_Empty()
        {
            var result = await _sut.GetEntryAsync(10);
            result.ShouldBeNull();
        }
    }
}
