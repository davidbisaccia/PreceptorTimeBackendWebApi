using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Requests.Validators.User
{
    public class LogOutRequestValidator : AbstractValidator<LogOutRequest>
    {
        public LogOutRequestValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.DisplayName).MinimumLength(8);
        }
    }
}
