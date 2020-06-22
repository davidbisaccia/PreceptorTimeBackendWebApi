using FluentValidation.TestHelper;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Requests.Validators.TimeEntry;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PreceptorTime.Domain.Tests.Validators.TimeEntry
{
    public class GetTimeEntryRequestValidatorTests
    {
        private readonly GetTimeEntryRequestValidator validator = new GetTimeEntryRequestValidator();

        [Fact]
        public void NegativeId()
        {
            var req = new GetTimeEntryRequest()
            {
                Id = -1,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Id, req);
        }

        [Fact]
        public void ZeroId()
        {
            var req = new GetTimeEntryRequest()
            {
                Id = 0,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Id, req);
        }

        [Fact]
        public void NoError()
        {
            var req = new GetTimeEntryRequest()
            {
                Id = 1,
            };

            validator.ShouldNotHaveValidationErrorFor(x => x.Id, req);
        }
    }
}
