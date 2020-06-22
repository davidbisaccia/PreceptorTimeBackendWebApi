using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Requests.Validators.User
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Password).MinimumLength(8);
        }
    }
}
