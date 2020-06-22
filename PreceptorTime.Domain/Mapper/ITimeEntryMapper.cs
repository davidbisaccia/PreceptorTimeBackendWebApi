using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Mapper
{
    public interface ITimeEntryMapper
    {
        TimeEntry Map(AddTimeEntryRequest req);
        TimeEntry Map(EditTimeEntryRequest req);
        TimeEntry Map(DeleteTimeEntryRequest req);
        TimeEntryResponse Map(TimeEntry entry);
    
    }
}
