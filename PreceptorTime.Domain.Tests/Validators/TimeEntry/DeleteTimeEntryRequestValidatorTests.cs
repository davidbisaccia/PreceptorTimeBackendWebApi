using FluentValidation.TestHelper;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Requests.Validators.TimeEntry;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PreceptorTime.Domain.Tests.Validators.TimeEntry
{
    public class DeleteTimeEntryRequestValidatorTests
    {
        private readonly DeleteTimeEntryRequestValidator validator = new DeleteTimeEntryRequestValidator();

        [Fact]
        public void InvalidDateInFuture()
        {
            var req = new DeleteTimeEntryRequest()
            {
                Date = new DateTime(3000, 1, 1).ToString(),
            };

            validator.ShouldHaveValidationErrorFor(x => x.Date, req);
        }

        [Fact]
        public void InvalidDateInPast()
        {
            var req = new DeleteTimeEntryRequest()
            {
                Date = new DateTime(1999, 1, 1).ToString(),
            };

            validator.ShouldHaveValidationErrorFor(x => x.Date, req);
        }

        [Fact]
        public void InvalidHoursNegative()
        {
            var req = new DeleteTimeEntryRequest()
            {
                Hours = -1,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Hours, req);
        }

        [Fact]
        public void InvalidHoursZero()
        {
            var req = new DeleteTimeEntryRequest()
            {
                Hours = 0,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Hours, req);
        }

        [Fact]
        public void InvalidPreceptorId()
        {
            var req = new DeleteTimeEntryRequest()
            {
                PreceptorId = 0,
            };

            validator.ShouldHaveValidationErrorFor(x => x.PreceptorId, req);
        }

        [Fact]
        public void InvalidStudentId()
        {
            var req = new DeleteTimeEntryRequest()
            {
                StudentId = 0,
            };

            validator.ShouldHaveValidationErrorFor(x => x.StudentId, req);
        }

        [Fact]
        public void InvalidRotationEmpty()
        {
            var req = new DeleteTimeEntryRequest()
            {
                Rotation = "",
            };

            validator.ShouldHaveValidationErrorFor(x => x.Rotation, req);
        }

        [Fact]
        public void InvalidRotationNull()
        {
            var req = new DeleteTimeEntryRequest()
            {
                Rotation = null,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Rotation, req);
        }
    }
}
