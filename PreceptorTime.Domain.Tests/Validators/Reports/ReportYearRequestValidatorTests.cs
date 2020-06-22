using FluentValidation.TestHelper;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Requests.Validators.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PreceptorTime.Domain.Tests.Validators.Reports
{
    public class ReportYearRequestValidatorTests
    {
        private readonly ReportYearRequestValidator validator = new ReportYearRequestValidator();


        [Fact]
        public void ShouldHaveErrorForYearBefore2000()
        {
            var req = new ReportYearRequest()
            {
                Year = 1999,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Year, req);
        }

        [Fact]
        public void ShouldHaveErrorForYearBefore3000()
        {
            var req = new ReportYearRequest()
            {
                Year = 3000,
            };

            validator.ShouldHaveValidationErrorFor(x => x.Year, req);
        }

        [Fact]
        public void ShouldBeValid()
        {
            var req = new ReportYearRequest()
            {
                Year = 2020,
            };

            validator.ShouldNotHaveValidationErrorFor(x => x.Year, req);
        }
    }
}
