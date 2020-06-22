using FluentValidation.TestHelper;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Requests.Validators.TimeEntry;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PreceptorTime.Domain.Tests.Validators.TimeEntry
{
    public class LearnerTimeEntryRequestValidatorTests
    {
        private readonly LearnerTimeEntryRequestValidator validator = new LearnerTimeEntryRequestValidator();

        [Fact]
        public void ShouldHaveErrorForNegativeID()
        {
            var req = new LearnerTimeEntryRequest()
            {
                Id = -1,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Id, req);
        }

        [Fact]
        public void ShouldHaveErrorForYearBefore2000()
        {
            var req = new LearnerTimeEntryRequest()
            {
                Year = 1999,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Year, req);
        }

        [Fact]
        public void ShouldHaveErrorForYearBefore3000()
        {
            var req = new LearnerTimeEntryRequest()
            {
                Year = 3000,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Year, req);
        }

        [Fact]
        public void ShouldBeValid()
        {
            var req = new LearnerTimeEntryRequest()
            {
                Id = 1,
                Year = 2020,
            };

            validator.ShouldNotHaveValidationErrorFor(x => x.Year, req);
            validator.ShouldNotHaveValidationErrorFor(x => x.Id, req);
        }
    }
}
