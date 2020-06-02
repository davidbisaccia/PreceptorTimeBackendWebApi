using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PreceptorTimeApi.DTO;

namespace PreceptorTimeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeController : ControllerBase
    {
        private const string IdParameter = "{id:int}";
        private readonly ILogger<TimeController> _logger;

        public TimeController(ILogger<TimeController> logger)
        {
            _logger = logger;
        }

        private IEnumerable<TimeEntryDto> FakeDataForTestingWithNoSQL()
        {
            var t1 = new TimeEntryDto()
            {
                Id = 1,
                PreceptorId = 1,
                StudentId = 2,
                Hours = 2,
                Notes = "Notes",
                Rotation = "GI",
                Date = DateTime.Now.ToUniversalTime().ToString("s"),
                //Date = DateTime.Now.ToString(//),//"MM/dd/yyyy", CultureInfo.InvariantCulture),
                PreceptorDisplayName = "Dave",
                StudentDisplayName = "Me",
            };
            yield return t1;

            var t2 = new TimeEntryDto()
            {
                Id = 2,
                PreceptorId = 3,
                StudentId = 2,
                Hours = 5,
                Notes = "notes asdsadas dasd sad asd asd ",
                Rotation = "Endo",
                Date = DateTime.Now.ToUniversalTime().ToString("s"),
                //Date = DateTime.Now.ToString(),//"MM/dd/yyyy", CultureInfo.InvariantCulture),
                PreceptorDisplayName = "Brad",
                StudentDisplayName = "Me",
            };
            yield return t2;

            var t3 = new TimeEntryDto()
            {
                Id = 3,
                PreceptorId = 4,
                StudentId = 5,
                Hours = 2,
                Notes = "notes",
                Rotation = "Endo",
                Date = DateTime.Now.ToUniversalTime().ToString("s"),//"MM/dd/yyyy", CultureInfo.InvariantCulture),
                PreceptorDisplayName = "MECEPTOR",
                StudentDisplayName = "peeps",
            };
            yield return t3;

            var t4 = new TimeEntryDto()
            {
                Id = 4,
                PreceptorId = 4,
                StudentId = 6,
                Hours = 4,
                Notes = "notes asdsadas dasd sad asd asd ",
                Rotation = "Endo",
                Date = DateTime.Now.ToUniversalTime().ToString("s"),
                //Date = DateTime.Now.ToString(),//"MM/dd/yyyy", CultureInfo.InvariantCulture),
                PreceptorDisplayName = "MECEPTOR",
                StudentDisplayName = "other peeps",
            };
            yield return t4;
        }

        [HttpGet("preceptors/{id:int}")]
        public IEnumerable<TimeEntryDto> GetPreceptorTimeEntries(int id)
        {
            return FakeDataForTestingWithNoSQL();
        }

        [HttpGet("learners/{id:int}")]
        public IEnumerable<TimeEntryDto> GetLearnerTimeEntries(int id)
        {
            return FakeDataForTestingWithNoSQL();
        }

        [HttpDelete(IdParameter)]
        public bool DeleteTimeEntry(int id)
        {
            return true;
        }

        [HttpPut]
        public int AddNewEntry([FromBody]TimeEntryDto timeEntry)
        {
            DateTime date = DateTime.Parse(timeEntry.Date);
            return 10;
        }

        [HttpPost]
        public bool EditEntry([FromBody]TimeEntryDto timeEntry)
        {
            return true;
        }
    }
}
