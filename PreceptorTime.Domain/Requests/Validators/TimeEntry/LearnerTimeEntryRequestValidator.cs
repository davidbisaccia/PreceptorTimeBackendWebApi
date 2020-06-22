using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Requests.Validators.TimeEntry
{
    public class LearnerTimeEntryRequestValidator : AbstractValidator<LearnerTimeEntryRequest>
    {
        public LearnerTimeEntryRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Year).GreaterThan(2000);
            RuleFor(x => x.Year).LessThan(DateTime.Now.Year + 1);
        }
    }
}
