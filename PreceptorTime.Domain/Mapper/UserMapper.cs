using PreceptorTime.Api.Converters;
using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Mapper
{
    public class UserMapper : IUserMapper
    {
        public User Map(RegisterUserRequest req)
        {
            if (req == null)
                return null;

            AccountType account;
            if (!Enum.TryParse<AccountType>(req.AccountType, out account))
                throw new ArgumentException($"Invalid Account Type: {req.AccountType}");

            return new User()
            {
                Account = account,
                DisplayName = req.DisplayName,
                Email = req.Email,
                Password = req.Password,
            };
        }

        public UserResponse Map(User user)
        {
            if (user == null)
                return null;

            var tokenExp = user.TokenExpiration.HasValue ?
                  DateConverter.Convert(user.TokenExpiration.Value)
                : string.Empty;

            return new UserResponse()
            {
                AccountType = user.Account.ToString(),
                DisplayName = user.DisplayName,
                Email = user.Email,
                Id = user.Id.ToString(),
                Token = user.Token,
                TokenExpirationDate = tokenExp,
            };
        }
    }
}
