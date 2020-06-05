using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreceptorTimeApi.Converters;
using PreceptorTimeApi.DTO;

namespace PreceptorTimeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public UserLoggedInDto LogIn([FromBody]LogInDto dto)
        {
            return new UserLoggedInDto()
            {
                DisplayName = dto.UserName,
                Email = "blah@somethingmail.com",
                Id = "1",
                Token = "token",
                TokenExpirationDate = DateConverter.Convert(DateTime.Now.AddSeconds(3600)),
                AccountType = "admin",
            };
        }

        [HttpPost("logout")]
        public bool LogOut([FromBody]LogOutDto dto)
        {
            return true;
        }

        [HttpPost("register")]
        public UserLoggedInDto Register([FromBody]RegisterDto dto)
        {
            return new UserLoggedInDto()
            {
                DisplayName = dto.UserName,
                Email = dto.Email,
                Id = "1",
                Token = "token",
                TokenExpirationDate = DateConverter.Convert(DateTime.Now.AddSeconds(3600)),
                AccountType = dto.AccountType,
            };
        }
    }
}