using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Mapper
{
    public class UserInfoMapper : IUserInfoMapper
    {
        public UserInfoResponse Map(User user)
        {
            if (user == null)
                return null;

            return new UserInfoResponse()
            {
                AccountType = user.Account.ToString(),
                Active = user.Active,
                DisplayName = user.DisplayName,
                Id = user.Id, //TODO: should I make this a string?
            };

        }
    }
}
