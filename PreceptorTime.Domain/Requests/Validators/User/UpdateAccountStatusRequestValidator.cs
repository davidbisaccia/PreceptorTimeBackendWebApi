using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Requests.Validators.User
{
    public class UpdateAccountStatusRequestValidator : AbstractValidator<UpdateAccountStatusRequest>
    {
        public UpdateAccountStatusRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
