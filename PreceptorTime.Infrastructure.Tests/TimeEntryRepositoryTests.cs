using Microsoft.EntityFrameworkCore;
using PreceptorTime.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Newtonsoft.Json;
using PreceptorTime.Domain.Entities;
using System.Linq;

namespace PreceptorTime.Infrastructure.Tests
{
    public class TimeEntryRepositoryTests
    {
        [Fact]
        public async Task GetAsync_Year2020_Success()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);
            var result = await sut.GetAsync(2020);
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Year2021_Empty()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);
            var result = await sut.GetAsync(2021);
            result.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetLearnerAsync_Id1_Year2020_Empty()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);
            var result = await sut.GetLearnerAsync(2020, 1);
            result.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetLearnerAsync_Id2_Year2021_Empty()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);
            var result = await sut.GetLearnerAsync(2021, 2);
            result.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetLearnerAsync_Id2_Year2020_Success()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);
            var result = await sut.GetLearnerAsync(2020, 2);
            result.ShouldNotBeEmpty();
        }


        //////////
        [Fact]
        public async Task GetTeacherAsync_Id2_Year2020_Empty()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);
            var result = await sut.GetTeacherAsync(2020, 2);
            result.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetTeacherAsync_Id1_Year2021_Empty()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);
            var result = await sut.GetTeacherAsync(2021, 1);
            result.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetTeacherAsync_Id1_Year2020_Success()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);
            var result = await sut.GetTeacherAsync(2020, 1);
            result.ShouldNotBeEmpty();
        }

        [Theory]
        [InlineData("{ \"Id\" : 1, \"TeacherId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"AnotherRotation\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task Add_NewTimeEntry_Failure(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<TimeEntry>(jsonEntity);

            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_add_new_items")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                sut.Add(entity);
                await sut.UnitOfWork.SaveEntitiesAsync();
            });
        }

        [Theory]
        [InlineData("{ \"Id\" : 7, \"TeacherId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"AnotherRotation\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task Add_NewTimeEntry_Success(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<TimeEntry>(jsonEntity);

            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_add_new_items")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);
            sut.Add(entity);

            await sut.UnitOfWork.SaveEntitiesAsync();

            context.TimeEntries
                .FirstOrDefault(x => x.Id == entity.Id)
                .ShouldNotBeNull();
        }

        [Theory]
        [InlineData("{ \"Id\" : 7, \"TeacherId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"AnotherRotation\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task Update_TimeEntry_Throws(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<TimeEntry>(jsonEntity);

            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_update_new_items")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>( async () =>
            {
                sut.Update(entity);
                await sut.UnitOfWork.SaveEntitiesAsync();
            });
        }

        [Theory]
        [InlineData("{ \"Id\" : 6,	\"TeacherId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"Rhuem\", \"Hours\" : 450, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task Update_TimeEntry_Success(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<TimeEntry>(jsonEntity);

            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_update_new_items")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);

            sut.Update(entity);
            await sut.UnitOfWork.SaveEntitiesAsync();

            context.TimeEntries
                .FirstOrDefault(x => x.Id == entity.Id)
                ?.Hours.ShouldBe(450);
        }

        [Theory]
        [InlineData("{ \"Id\" : 6,	\"TeacherId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"Rhuem\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task Delete_TimeEntry_Success(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<TimeEntry>(jsonEntity);

            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_delete_exisiting_items")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);

            sut.Delete(entity);
            await sut.UnitOfWork.SaveEntitiesAsync();

            context.TimeEntries
                .FirstOrDefault(x => x.Id == entity.Id)
                .ShouldBeNull();
        }

        [Theory]
        [InlineData("{ \"Id\" : 7,	\"TeacherId\" : 3, \"StudentId\" : 6, \"Rotation\" : \"Rhuem\", \"Hours\" : 412, \"Notes\" : \"Notes test\", \"Date\" : \"2020-06-11T02:35:16\" }")]
        public async Task Delete_TimeEntry_Failure(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<TimeEntry>(jsonEntity);

            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_delete_exisiting_items")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var sut = new TimeEntryRepository(context);

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
            {
                sut.Delete(entity);
                await sut.UnitOfWork.SaveEntitiesAsync();
            });
        }
    }
}
