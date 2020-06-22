using FluentValidation.TestHelper;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Requests.Validators.TimeEntry;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PreceptorTime.Domain.Tests.Validators.TimeEntry
{
    public class EditTimeEntryReqestValidatorTests
    {
        private readonly EditTimeEntryRequestValidator validator = new EditTimeEntryRequestValidator();

        [Fact]
        public void InvalidDateInFuture()
        {
            var req = new EditTimeEntryRequest()
            {
                Date = new DateTime(3000, 1, 1).ToString(),
            };

            validator.ShouldHaveValidationErrorFor(x => x.Date, req);
        }

        [Fact]
        public void InvalidDateInPast()
        {
            var req = new EditTimeEntryRequest()
            {
                Date = new DateTime(1999, 1, 1).ToString(),
            };

            validator.ShouldHaveValidationErrorFor(x => x.Date, req);
        }

        [Fact]
        public void InvalidHoursNegative()
        {
            var req = new EditTimeEntryRequest()
            {
                Hours = -1,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Hours, req);
        }

        [Fact]
        public void InvalidHoursZero()
        {
            var req = new EditTimeEntryRequest()
            {
                Hours = 0,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Hours, req);
        }

        [Fact]
        public void InvalidPreceptorId()
        {
            var req = new EditTimeEntryRequest()
            {
                PreceptorId = 0,
            };

            validator.ShouldHaveValidationErrorFor(x => x.PreceptorId, req);
        }

        [Fact]
        public void InvalidStudentId()
        {
            var req = new EditTimeEntryRequest()
            {
                StudentId = 0,
            };

            validator.ShouldHaveValidationErrorFor(x => x.StudentId, req);
        }

        [Fact]
        public void InvalidRotationEmpty()
        {
            var req = new EditTimeEntryRequest()
            {
                Rotation = "",
            };

            validator.ShouldHaveValidationErrorFor(x => x.Rotation, req);
        }

        [Fact]
        public void InvalidRotationNull()
        {
            var req = new EditTimeEntryRequest()
            {
                Rotation = null,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Rotation, req);
        }
    }
}
