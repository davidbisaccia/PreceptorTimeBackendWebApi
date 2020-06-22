using FluentValidation;
using PreceptorTime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Requests.Validators.User
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.DisplayName).MinimumLength(8);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).MinimumLength(8);
            RuleFor(x => x.AccountType).Must(x =>
            {
                AccountType account;
                return Enum.TryParse<AccountType>(x, out account);
            });
        }
    }
}
