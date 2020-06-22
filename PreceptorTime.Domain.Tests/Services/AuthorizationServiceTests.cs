using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Mapper;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Services;
using PreceptorTime.Fixtures;
using PreceptorTime.Infrastructure.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace PreceptorTime.Domain.Tests
{
    public class AuthorizationServiceTests : IClassFixture<PreceptorTimeContextFactory>
    {
        private readonly UserRepository userRepo;
        private readonly UserMapper userMapper;

        public AuthorizationServiceTests(PreceptorTimeContextFactory factory)
        {
            userRepo = new UserRepository(factory.ContextInstance);
            userMapper = new UserMapper();
        }

        [Theory]
        [InlineData("passno", "dave", false)]
        [InlineData("password", "dave", true)]
        public async Task LogInTests(string password, string username, bool success)
        {
            var req = new LogInRequest()
            {
                Password = password,
                UserName = username,
            };

            AuthorizationService service = new AuthorizationService(userRepo, userMapper);
            var result = await service.LogInAsync(req);
            if(success)
            {
                result.ShouldNotBeNull();
            }
            else
            {
                result.ShouldBeNull();
            }
        }

        [Theory]
        [InlineData("passno", "no_one")]
        public async Task LogInFailure (string password, string username)
        {
            var req = new LogInRequest()
            {
                Password = password,
                UserName = username,
            };

            AuthorizationService service = new AuthorizationService(userRepo, userMapper);
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                var result = await service.LogInAsync(req);
            });
        }

        [Theory]
        [InlineData("password", "newName", "email", "full name", AccountType.Student)]
        public async Task RegisterSuccess(string password, string username, string email, string name, AccountType a)
        {
            var req = new RegisterUserRequest()
            {
                Password = password,
                AccountType = a.ToString(),
                Email = email,
                DisplayName = name,
            };

            AuthorizationService service = new AuthorizationService(userRepo, userMapper);
            var resp = await service.RegisterAsync(req);
            resp.ShouldNotBeNull();
        }

        //TODO: should we have this test?
        //Probably, but will it not work correctly with in memory database?
        //[Theory]
        ////these should be unique and will not be with the username and the email
        //[InlineData("password", "dave", "email", "full name", AccountType.Student)]
        //[InlineData("password", "unique", "dave", "full name", AccountType.Student)]
        //public async Task RegisterFailure(string password, string username, string email, string name, AccountType a)
        //{
        //    var req = new RegisterUserRequest()
        //    {
        //        Password = password,
        //        UserName = username,
        //        AccountType = a.ToString(),
        //        Email = email,
        //        Name = name,
        //    };

        //    AuthorizationService service = new AuthorizationService(userRepo, userMapper);

        //    await Assert.ThrowsAsync<Exception>(async () =>
        //    {
        //        var resp = await service.RegisterAsync(req);
        //    });
        //}

        [Theory]
        [InlineData("failure", "dave", false)]
        [InlineData("token", "dave", true)]
        public async Task LogOutTests(string token, string username, bool success)
        {
            var req = new LogOutRequest()
            {
                DisplayName = username,
                Token = token,
            };

            AuthorizationService service = new AuthorizationService(userRepo, userMapper);
            var result = await service.LogOutAsync(req);
            Assert.True(result == success);
        }

        [Theory]
        [InlineData("token", "no_one")]
        public async Task LogOutFailure(string token, string username)
        {
            var req = new LogOutRequest()
            {
                DisplayName = username,
                Token = token,
            };

            AuthorizationService service = new AuthorizationService(userRepo, userMapper);
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                var result = await service.LogOutAsync(req);
            });
        }
    }
}
