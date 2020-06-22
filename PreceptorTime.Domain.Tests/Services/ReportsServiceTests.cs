using PreceptorTime.Domain.Mapper;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Services;
using PreceptorTime.Fixtures;
using PreceptorTime.Infrastructure.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PreceptorTime.Domain.Tests
{
    public class ReportsServiceTests : IClassFixture<PreceptorTimeContextFactory>
    {
        private readonly TimeEntryRepository timeRepo;
        private readonly ReportMapper reportMapper;

        public ReportsServiceTests(PreceptorTimeContextFactory factory)
        {
            timeRepo = new TimeEntryRepository(factory.ContextInstance);
            reportMapper = new ReportMapper();
        }

        [Fact]
        public async Task GetAvilableYears()
        {
            ReportsService service = new ReportsService(timeRepo, reportMapper);
            var result = await service.GetAvilableYears();
            result.ShouldNotBeEmpty();
        }

        [Theory]
        [InlineData(2, 2020, true)]
        [InlineData(2, 2021, false)]
        [InlineData(20, 2021, false)]
        public async Task GetLearnerReportsAsync(int id, int year, bool success)
        {
            var req = new LearnerReportRequest()
            {
                Id = id,
                Year = year,
            };
            ReportsService service = new ReportsService(timeRepo, reportMapper);
            var result = await service.GetLearnerReportsAsync(req);
            if (success)
            {
                result.ShouldNotBeEmpty();
            }
            else
            {
                result.ShouldBeEmpty();
            }
        }

        [Theory]
        [InlineData(1, 2020, true)]
        [InlineData(1, 2021, false)]
        [InlineData(10, 2021, false)]
        public async Task GetPreceptorReportsAsync(int id, int year, bool success)
        {
            var req = new PreceptorReportRequest()
            {
                Id = id,
                Year = year,
            };
            ReportsService service = new ReportsService(timeRepo, reportMapper);
            var result = await service.GetPreceptorReportsAsync(req);
            if (success)
            {
                result.ShouldNotBeEmpty();
            }
            else
            {
                result.ShouldBeEmpty();
            }
        }

        [Theory]
        [InlineData(2020, true)]
        [InlineData(2021, false)]
        public async Task GetYearReportsAsync(int year, bool success)
        {
            var req = new ReportYearRequest()
            {
                Year = year,
            };
            ReportsService service = new ReportsService(timeRepo, reportMapper);
            var result = await service.GetYearReportsAsync(req);
            if (success)
            {
                result.ShouldNotBeEmpty();
            }
            else
            {
                result.ShouldBeEmpty();
            }
        }
    }
}
