using Microsoft.EntityFrameworkCore;
using PreceptorTime.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Newtonsoft.Json;
using PreceptorTime.Domain.Entities;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;

namespace PreceptorTime.Infrastructure.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task GetAsync_Success()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var userRepo = new UserRepository(context);
            var result = await userRepo.GetAsync();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Id1_Success()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var userRepo = new UserRepository(context);
            var result = await userRepo.GetAsync(1);
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Id7_Failure()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var userRepo = new UserRepository(context);
            var result = await userRepo.GetAsync(7);
            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetAsync_Admins_Success()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var userRepo = new UserRepository(context);

            var actTypes = new List<AccountType>() { AccountType.Admin };
            var result = await userRepo.GetAsync(actTypes);
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Preceptor_Success()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var userRepo = new UserRepository(context);

            var actTypes = new List<AccountType>() { AccountType.Preceptor };
            var result = await userRepo.GetAsync(actTypes);
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Resident_Success()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var userRepo = new UserRepository(context);

            var actTypes = new List<AccountType>() { AccountType.Resident };
            var result = await userRepo.GetAsync(actTypes);
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Student_Success()
        {
            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var userRepo = new UserRepository(context);

            var actTypes = new List<AccountType>() { AccountType.Student };
            var result = await userRepo.GetAsync(actTypes);
            result.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("{ \"Id\" : 7, \"DisplayName\" : \"new user\", \"AccountType\" : \"student\", \"Active\" : \"true\", \"Email\" : \"dave\", \"Token\" : \"token\", \"TokenExpiration\" : \"\" }")]
        public async Task Add_User_Success(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<User>(jsonEntity);

            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_add_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var userRepo = new UserRepository(context);

            var actTypes = new List<AccountType>() { AccountType.Student };
            userRepo.Add(entity);
            await userRepo.UnitOfWork.SaveChangesAsync();

            context.Users
                .FirstOrDefault(u => u.Id == entity.Id)
                .ShouldNotBeNull();
        }

        [Theory]
        [InlineData("{ \"Id\" : 1, \"DisplayName\" : \"new user\", \"AccountType\" : \"student\", \"Active\" : \"true\", \"Email\" : \"dave\", \"Token\" : \"token\", \"TokenExpiration\" : \"\" }")]
        public async Task Add_User_Failure(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<User>(jsonEntity);

            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_add_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var userRepo = new UserRepository(context);

            var actTypes = new List<AccountType>() { AccountType.Student };
            
            await Assert.ThrowsAsync<ArgumentException>( async () => 
            { 
                userRepo.Add(entity);
                await userRepo.UnitOfWork.SaveChangesAsync();
            });
        }

        [Theory]
        [InlineData("{ \"Id\" : 7, \"DisplayName\" : \"new user\", \"AccountType\" : \"student\", \"Active\" : \"true\", \"Email\" : \"dave\", \"Token\" : \"token\", \"TokenExpiration\" : \"\" }")]
        public async Task Update_User_Failure(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<User>(jsonEntity);

            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_add_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var userRepo = new UserRepository(context);

            var actTypes = new List<AccountType>() { AccountType.Student };

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
            {
                userRepo.Update(entity);
                await userRepo.UnitOfWork.SaveChangesAsync();
            });
        }

        [Theory]
        [InlineData("{ \"Id\" : 1, \"DisplayName\" : \"new user\", \"AccountType\" : \"student\", \"Active\" : \"false\", \"Email\" : \"dave\", \"Token\" : \"token\", \"TokenExpiration\" : \"\" }")]
        public async Task Update_User_Success(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<User>(jsonEntity);

            var options = new DbContextOptionsBuilder<PreceptorTimeContext>()
                .UseInMemoryDatabase(databaseName: "should_update_data")
                .Options;

            await using var context = new TestPreceptorTimeContext(options);
            context.Database.EnsureCreated();

            var userRepo = new UserRepository(context);

            var actTypes = new List<AccountType>() { AccountType.Student };

            userRepo.Update(entity);
            await userRepo.UnitOfWork.SaveChangesAsync();

            context.Users
                .FirstOrDefault(u => u.Id == entity.Id)
                ?.Active.ShouldBeFalse();
        }
    }
}
