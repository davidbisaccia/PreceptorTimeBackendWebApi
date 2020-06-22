using FluentValidation;
using System;

namespace PreceptorTime.Domain.Requests.Validators.TimeEntry
{
    public class GetTimeEntryRequestValidator : AbstractValidator<GetTimeEntryRequest>
    {
        public GetTimeEntryRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
