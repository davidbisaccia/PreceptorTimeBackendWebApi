using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Requests.Validators.User
{
    public class LogInRequestValidator : AbstractValidator<LogInRequest>
    {
        public LogInRequestValidator()
        {
            RuleFor(x => x.Password).MinimumLength(8);
            RuleFor(x => x.UserName).MinimumLength(8);
        }
    }
}
