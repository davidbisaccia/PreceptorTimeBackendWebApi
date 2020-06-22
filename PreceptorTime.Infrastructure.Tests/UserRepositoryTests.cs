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
using PreceptorTime.Fixtures;

namespace PreceptorTime.Infrastructure.Tests
{
    public class UserRepositoryTests : IClassFixture<PreceptorTimeContextFactory>
    {
        private readonly UserRepository _userRepo;
        private readonly TestPreceptorTimeContext _context;

        public UserRepositoryTests(PreceptorTimeContextFactory factory)
        {
            _context = factory.ContextInstance;
            _userRepo = new UserRepository(_context);
        }


        [Fact]
        public async Task GetAsync_Success()
        {
            var result = await _userRepo.GetAsync();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Id1_Success()
        {
            var result = await _userRepo.GetAsync(1);
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Id7_Failure()
        {
            var result = await _userRepo.GetAsync(7);
            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetAsync_Admins_Success()
        {
            var actTypes = new List<AccountType>() { AccountType.Admin };
            var result = await _userRepo.GetAsync(actTypes);
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Preceptor_Success()
        {
            var actTypes = new List<AccountType>() { AccountType.Preceptor };
            var result = await _userRepo.GetAsync(actTypes);
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Resident_Success()
        {
            var actTypes = new List<AccountType>() { AccountType.Resident };
            var result = await _userRepo.GetAsync(actTypes);
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Student_Success()
        {
            var actTypes = new List<AccountType>() { AccountType.Student };
            var result = await _userRepo.GetAsync(actTypes);
            result.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("{ \"Id\" : 7, \"DisplayName\" : \"new user\", \"AccountType\" : \"student\", \"Active\" : \"true\", \"Email\" : \"dave\", \"Token\" : \"token\", \"TokenExpiration\" : \"\" }")]
        public async Task Add_User_Success(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<User>(jsonEntity);
            var actTypes = new List<AccountType>() { AccountType.Student };
            _userRepo.Add(entity);
            await _userRepo.UnitOfWork.SaveChangesAsync();

            _context.Users
                .FirstOrDefault(u => u.Id == entity.Id)
                .ShouldNotBeNull();
        }

        [Theory]
        [InlineData("{ \"Id\" : 1, \"DisplayName\" : \"new user\", \"AccountType\" : \"student\", \"Active\" : \"true\", \"Email\" : \"dave\", \"Token\" : \"token\", \"TokenExpiration\" : \"\" }")]
        public async Task Add_User_Failure(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<User>(jsonEntity);
            var actTypes = new List<AccountType>() { AccountType.Student };
            
            await Assert.ThrowsAsync<ArgumentException>( async () => 
            { 
                _userRepo.Add(entity);
                await _userRepo.UnitOfWork.SaveChangesAsync();
            });
        }

        [Theory]
        [InlineData("{ \"Id\" : 7, \"DisplayName\" : \"new user\", \"AccountType\" : \"student\", \"Active\" : \"true\", \"Email\" : \"dave\", \"Token\" : \"token\", \"TokenExpiration\" : \"\" }")]
        public async Task Update_User_Failure(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<User>(jsonEntity);
            var actTypes = new List<AccountType>() { AccountType.Student };

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
            {
                _userRepo.Update(entity);
                await _userRepo.UnitOfWork.SaveChangesAsync();
            });
        }

        [Theory]
        [InlineData("{ \"Id\" : 1, \"DisplayName\" : \"new user\", \"AccountType\" : \"student\", \"Active\" : \"false\", \"Email\" : \"dave\", \"Token\" : \"token\", \"TokenExpiration\" : \"\" }")]
        public async Task Update_User_Success(string jsonEntity)
        {
            var entity = JsonConvert.DeserializeObject<User>(jsonEntity);
            var actTypes = new List<AccountType>() { AccountType.Student };

            _userRepo.Update(entity);
            await _userRepo.UnitOfWork.SaveChangesAsync();

            _context.Users
                .FirstOrDefault(u => u.Id == entity.Id)
                ?.Active.ShouldBeFalse();
        }
    }
}
