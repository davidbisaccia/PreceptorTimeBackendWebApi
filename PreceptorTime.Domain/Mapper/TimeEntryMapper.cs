using PreceptorTime.Api.Converters;
using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Mapper
{
    public class TimeEntryMapper : ITimeEntryMapper
    {
        private readonly IUserMapper _userMapper;

        public TimeEntryMapper(IUserMapper userMapper)
        {
            _userMapper = userMapper;
        }

        public TimeEntry Map(AddTimeEntryRequest req)
        {
            if (req == null)
                return null;

            return new TimeEntry()
            {
                Date = DateTime.Parse(req.Date),
                Hours = req.Hours,
                Notes = req.Notes,
                Rotation = req.Rotation,
                StudentId = req.StudentId,
                TeacherId = req.PreceptorId,
            };
        }

        public TimeEntry Map(EditTimeEntryRequest req)
        {
            if (req == null)
                return null;

            return new TimeEntry()
            {
                Id = req.Id,
                Date = DateTime.Parse(req.Date),
                Hours = req.Hours,
                Notes = req.Notes,
                Rotation = req.Rotation,
                StudentId = req.StudentId,
                TeacherId = req.StudentId,
            };
        }

        public TimeEntryResponse Map(TimeEntry entry)
        {
            if (entry == null)
                return null;

            return new TimeEntryResponse()
            {
                Date = DateConverter.Convert(entry.Date),
                Hours = entry.Hours,
                Id = entry.Id,
                Notes = entry.Notes,
                Rotation = entry.Rotation,
                PreceptorId = entry.TeacherId,
                PreceptorDisplayName = entry.Teacher.DisplayName,
                StudentId = entry.StudentId,
                StudentDisplayName = entry.Student.DisplayName,
            };
        }

        public TimeEntry Map(DeleteTimeEntryRequest req)
        {
            if (req == null)
                return null;

            return new TimeEntry()
            {
                Id = req.Id,
                Date = DateTime.Parse(req.Date),
                Hours = req.Hours,
                Notes = req.Notes,
                Rotation = req.Rotation,
                StudentId = req.StudentId,
                TeacherId = req.StudentId,
            };
        }
    }
}
