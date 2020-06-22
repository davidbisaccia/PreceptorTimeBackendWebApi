using PreceptorTime.Domain.Mapper;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Services;
using PreceptorTime.Fixtures;
using PreceptorTime.Infrastructure.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PreceptorTime.Domain.Tests
{
    public class UserServiceTests : IClassFixture<PreceptorTimeContextFactory>
    {
        private readonly UserRepository userRepo;
        private readonly UserInfoMapper userMapper;

        public UserServiceTests(PreceptorTimeContextFactory factory)
        {
            userRepo = new UserRepository(factory.ContextInstance);
            userMapper = new UserInfoMapper();
        }

        [Fact]
        public async Task GetLearnersAsync()
        {
            var service = new UserService(userMapper, userRepo);
            var result = await service.GetLearnersAsync();
            result.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetPreceptorsAsync()
        {
            var service = new UserService(userMapper, userRepo);
            var result = await service.GetPreceptorsAsync();
            result.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetUsersAsync()
        {
            var service = new UserService(userMapper, userRepo);
            var result = await service.GetUsersAsync();
            result.ShouldNotBeEmpty();
        }

        [Theory]
        [InlineData(1, "test")]
        public async Task ResetUserPasswordAsync_Success(int id, string password)
        {
            var req = new ResetPasswordRequest()
            {
                Id = id,
                Password = password,
            };
            var service = new UserService(userMapper, userRepo);
            var result = await service.ResetUserPasswordAsync(req);
            result.ShouldBeTrue();
        }

        [Theory]
        [InlineData(10, "test")]
        public async Task ResetUserPasswordAsync_Faiure(int id, string password)
        {
            var req = new ResetPasswordRequest()
            {
                Id = id,
                Password = password,
            };
            var service = new UserService(userMapper, userRepo);
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                var result = await service.ResetUserPasswordAsync(req);
            });
        }

        [Theory]
        [InlineData(2, true)]
        [InlineData(3, false)]
        public async Task UpdateUserAccountStatus_Success(int id, bool active)
        {
            var req = new UpdateAccountStatusRequest()
            {
                Id = id,
                Active = active,
            };

            var service = new UserService(userMapper, userRepo);
            var result = await service.UpdateUserAccountStatus(req);
            result.ShouldBeTrue();
        }

        [Theory]
        [InlineData(10, true)]
        public async Task UpdateUserAccountStatus_Failure(int id, bool active)
        {
            var req = new UpdateAccountStatusRequest()
            {
                Id = id,
                Active = active,
            };

            var service = new UserService(userMapper, userRepo);
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                var result = await service.UpdateUserAccountStatus(req);
            });
        }
    }
}
