using FluentValidation.TestHelper;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Requests.Validators.TimeEntry;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PreceptorTime.Domain.Tests.Validators.TimeEntry
{
    public class PreceptorTimeEntryRequestValidatorTests
    {
        private readonly PreceptorTimeEntryRequestValidator validator = new PreceptorTimeEntryRequestValidator();

        [Fact]
        public void ShouldHaveErrorForNegativeID()
        {
            var req = new PreceptorTimeEntryRequest()
            {
                Id = -1,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Id, req);
        }

        [Fact]
        public void ShouldHaveErrorForYearBefore2000()
        {
            var req = new PreceptorTimeEntryRequest()
            {
                Year = 1999,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Year, req);
        }

        [Fact]
        public void ShouldHaveErrorForYearBefore3000()
        {
            var req = new PreceptorTimeEntryRequest()
            {
                Year = 3000,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Year, req);
        }

        [Fact]
        public void ShouldBeValid()
        {
            var req = new PreceptorTimeEntryRequest()
            {
                Id = 1,
                Year = 2020,
            };

            validator.ShouldNotHaveValidationErrorFor(x => x.Year, req);
            validator.ShouldNotHaveValidationErrorFor(x => x.Id, req);
        }
    }
}
