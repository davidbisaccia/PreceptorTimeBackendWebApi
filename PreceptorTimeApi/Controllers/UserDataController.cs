using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreceptorTimeApi.DTO;

namespace PreceptorTimeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private IEnumerable<UserInfoDto> SetupFakeDataForTestingWithNoSql()
        {
            yield return new UserInfoDto(){
                Id = 1, 
                DisplayName = "dave", 
                AccountType = "preceptor",
                Active = true,
            };

            yield return new UserInfoDto()
            {
                Id = 2,
                DisplayName = "me",
                AccountType = "student",
                Active = true,
            };

            yield return new UserInfoDto()
            {
                Id = 3,
                DisplayName = "Brad",
                AccountType = "admin",
                Active = true,
            };

            yield return new UserInfoDto()
            {
                Id = 4,
                DisplayName = "MECEPTOR",
                AccountType = "preceptor",
                Active = true,
            };

            yield return new UserInfoDto()
            {
                Id = 5,
                DisplayName = "peeps",
                AccountType = "resident",
                Active = true,
            };

            yield return new UserInfoDto()
            {
                Id = 6,
                DisplayName = "other peeps",
                AccountType = "student",
                Active = true,
            };
        }

        [HttpGet("preceptors")]
        public IEnumerable<UserInfoDto> GetPreceptors()
        {
            var userList = SetupFakeDataForTestingWithNoSql();
            
            return (from n in userList 
                    where n.AccountType == "preceptor" || n.AccountType == "admin" 
                    select n).ToList();
        }

        [HttpGet("learners")]
        public IEnumerable<UserInfoDto> GetLearners()
        {
            var userList = SetupFakeDataForTestingWithNoSql();

            return (from n in userList
                    where n.AccountType == "student" || n.AccountType == "resident"
                    select n).ToList();
        }

        [HttpGet("users")]
        public IEnumerable<UserInfoDto> GetUsers()
        {
            return SetupFakeDataForTestingWithNoSql().ToList();
        }

        [HttpPost("reset")]
        public bool ResetUserPassword([FromBody]ResetPasswordDto resetDto)
        {
            return true;
        }

        [HttpPost("update")]
        public bool UpdateAccountStatus([FromBody]UpdateAccountStatusDto resetDto)
        {
            return true;
        }
    }
}