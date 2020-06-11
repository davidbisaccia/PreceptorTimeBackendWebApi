using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PreceptorTime.Api.DTO;

namespace PreceptorTime.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        #region Fake Test Data
        IEnumerable<ReportDto> FakeDataForTestingPreceptors()
        {
            yield return new ReportDto()
            {
                Preceptor = "preceptor1",
                Learner = "student name",
                Rotation = "GI",
                TotalHours = 33
            };

            yield return new ReportDto()
            {
                Preceptor = "preceptor1",
                Learner = "student name2",
                Rotation = "Admin",
                TotalHours = 278
            };

            yield return new ReportDto()
            {
                Preceptor = "preceptor1",
                Learner = "student name3",
                Rotation = "GI",
                TotalHours = 22
            };

            yield return new ReportDto()
            {
                Preceptor = "preceptor2",
                Learner = "student name3",
                Rotation = "Nefro",
                TotalHours = 11
            };
        }

        IEnumerable<ReportDto> FakeDataForTestingLearners()
        {
            yield return new ReportDto()
            {
                Preceptor = "preceptor1",
                Learner = "student name",
                Rotation = "Nuero",
                TotalHours = 26
            };

            yield return new ReportDto()
            {
                Preceptor = "preceptor2",
                Learner = "student name",
                Rotation = "Rhuem",
                TotalHours = 412
            };
        }

        #endregion

        [HttpGet("preceptors/{id:int}/{year:int}")]
        public IEnumerable<ReportDto> GetPreceptorTimeEntries(int id, int year)
        {
            //TODO: handle id = 0 for all
            return FakeDataForTestingPreceptors().ToList();
        }

        [HttpGet("learners/{id:int}/{year:int}")]
        public IEnumerable<ReportDto> GetLearnerTimeEntries(int id, int year)
        {
            return FakeDataForTestingPreceptors().ToList();
        }

        [HttpGet("all/{year:int}")]
        public IEnumerable<ReportDto> GetAllTimeEntries(int year)
        {
            return FakeDataForTestingPreceptors().Concat(FakeDataForTestingLearners()).ToList();
        }

        [HttpGet("years")]
        public IEnumerable<int> GetAvailableYears()
        {
            return new List<int>()
            {
                2017,
                2018,
                2019,
                2020,
            };
        }
    }
}