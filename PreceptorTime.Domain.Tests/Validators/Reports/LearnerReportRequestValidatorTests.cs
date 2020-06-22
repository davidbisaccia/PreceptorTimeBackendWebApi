using FluentValidation.TestHelper;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Requests.Validators.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PreceptorTime.Domain.Tests.Validators.Reports
{
    public class LearnerReportRequestValidatorTests
    {
        private readonly LearnerReportRequestValidator validator = new LearnerReportRequestValidator();

        [Fact]
        public void ShouldHaveErrorForNegativeID()
        {
            var req = new LearnerReportRequest()
            {
                Id = -1,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Id, req);
        }

        [Fact]
        public void ShouldHaveErrorForYearBefore2000()
        {
            var req = new LearnerReportRequest()
            {
                Year = 1999,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Year, req);
        }

        [Fact]
        public void ShouldHaveErrorForYearBefore3000()
        {
            var req = new LearnerReportRequest()
            {
                Year = 3000,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Year, req);
        }

        [Fact]
        public void ShouldBeValid()
        {
            var req = new LearnerReportRequest()
            {
                Id = 1,
                Year = 2020,
            };

            validator.ShouldNotHaveValidationErrorFor(x => x.Year, req);
            validator.ShouldNotHaveValidationErrorFor(x => x.Id, req);
        }
    }
}
