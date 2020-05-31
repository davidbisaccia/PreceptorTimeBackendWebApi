using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PreceptorTimeApi.DTO;

namespace PreceptorTimeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private const string IdParameter = "{id:int}";
        private readonly ILogger<TimeController> _logger;

        public TimeController(ILogger<TimeController> logger)
        {
            _logger = logger;
        }

        private IEnumerable<TimeEntryDTO> FakeDataForTestingWithNoSQL()
        {
            var t1 = new TimeEntryDTO()
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

            var t2 = new TimeEntryDTO()
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

            var t3 = new TimeEntryDTO()
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

            var t4 = new TimeEntryDTO()
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
        public IEnumerable<TimeEntryDTO> GetPreceptorTimeEntries(int id)
        {
            return FakeDataForTestingWithNoSQL();
        }

        [HttpGet("learners/{id:int}")]
        public IEnumerable<TimeEntryDTO> GetLearnerTimeEntries(int id)
        {
            return FakeDataForTestingWithNoSQL();
        }

        [HttpDelete(IdParameter)]
        public bool DeleteTimeEntry(int id)
        {
            return true;
        }

        [HttpPut]
        public int AddNewEntry([FromBody]TimeEntryDTO timeEntry)
        {
            DateTime date = DateTime.Parse(timeEntry.Date);//,"dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return 10;
        }

        [HttpPost]
        public bool EditEntry([FromBody]TimeEntryDTO timeEntry)
        {
            return true;
        }
    }
}
