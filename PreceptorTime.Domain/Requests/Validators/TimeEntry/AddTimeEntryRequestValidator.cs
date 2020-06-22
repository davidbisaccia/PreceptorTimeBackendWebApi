using FluentValidation;
using PreceptorTime.Domain.Requests;
using System;

namespace Catalog.Domain.Requests.Validators
{
    public class AddTimeEntryRequestValidator : AbstractValidator<AddTimeEntryRequest>
    {
        public AddTimeEntryRequestValidator()
        {
            RuleFor(x => x.Date).Must(x =>
            {
                if (string.IsNullOrEmpty(x))
                    return false;

                DateTime d;
                if (DateTime.TryParse(x, out d))
                    return d.Year >= 2000 && d.Year < DateTime.Now.Year + 1;

                return false;
            });

            RuleFor(x => x.Hours).GreaterThan(0);
            RuleFor(x => x.PreceptorId).GreaterThan(0);
            RuleFor(x => x.Rotation).NotEmpty();
            RuleFor(x => x.StudentId).GreaterThan(0);
        }
    }
}
