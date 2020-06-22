using Catalog.Domain.Requests.Validators;
using FluentValidation.TestHelper;
using PreceptorTime.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PreceptorTime.Domain.Tests.Validators.TimeEntry
{
    public class AddTimeEntryRequestValidatorTests
    {
        private readonly AddTimeEntryRequestValidator validator = new AddTimeEntryRequestValidator();

        [Fact]
        public void InvalidDateInFuture()
        {
            var req = new AddTimeEntryRequest()
            {
                Date = new DateTime(3000, 1, 1).ToString(),
            };

            validator.ShouldHaveValidationErrorFor(x => x.Date, req);
        }

        [Fact]
        public void InvalidDateInPast()
        {
            var req = new AddTimeEntryRequest()
            {
                Date = new DateTime(1999, 1, 1).ToString(),
            };

            validator.ShouldHaveValidationErrorFor(x => x.Date, req);
        }

        [Fact]
        public void InvalidHoursNegative()
        {
            var req = new AddTimeEntryRequest()
            {
                Hours = -1,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Hours, req);
        }

        [Fact]
        public void InvalidHoursZero()
        {
            var req = new AddTimeEntryRequest()
            {
                Hours = 0,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Hours, req);
        }

        [Fact]
        public void InvalidPreceptorId()
        {
            var req = new AddTimeEntryRequest()
            {
                PreceptorId = 0,
            };

            validator.ShouldHaveValidationErrorFor(x => x.PreceptorId, req);
        }

        [Fact]
        public void InvalidStudentId()
        {
            var req = new AddTimeEntryRequest()
            {
                StudentId = 0,
            };

            validator.ShouldHaveValidationErrorFor(x => x.StudentId, req);
        }

        [Fact]
        public void InvalidRotationEmpty()
        {
            var req = new AddTimeEntryRequest()
            {
                Rotation = "",
            };

            validator.ShouldHaveValidationErrorFor(x => x.Rotation, req);
        }

        [Fact]
        public void InvalidRotationNull()
        {
            var req = new AddTimeEntryRequest()
            {
                Rotation = null,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Rotation, req);
        }
    }
}
