using PreceptorTime.Domain.Mapper;
using PreceptorTime.Domain.Repositories;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PreceptorTime.Domain.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly ITimeEntryMapper _mapper;
        private readonly ITimeEntryRepository _repo;

        public TimeEntryService(ITimeEntryMapper mapper, ITimeEntryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
             
        public async Task<TimeEntryResponse> AddTimeEntryAsync(AddTimeEntryRequest req)
        {
            var entry = _mapper.Map(req);
            var result = _repo.Add(entry);
            await _repo.UnitOfWork.SaveChangesAsync();

            //TODO: Look for all confirm results, and hopefully one day get rid of them
            //the add is not linking the foreign key objects.... 
            var confirmResult = await _repo.GetEntryAsync(result.Id);
            return _mapper.Map(confirmResult);
        }

        public async Task<bool> DeleteTimeEntryAsync(DeleteTimeEntryRequest req)
        {
            var entry = _mapper.Map(req);
            var result = await _repo.Delete(entry);
            await _repo.UnitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<TimeEntryResponse> EditTimeEntryAsync(EditTimeEntryRequest req)
        {
            var entry = _mapper.Map(req);
            var result = _repo.Update(entry);
            await _repo.UnitOfWork.SaveChangesAsync();

            var confirmResult = await _repo.GetEntryAsync(req.Id);
            return _mapper.Map(confirmResult);
        }

        public async Task<IEnumerable<TimeEntryResponse>> GetLearnerTimeEntriesAsync(LearnerTimeEntryRequest req)
        {
            var result = await _repo.GetLearnerAsync(req.Year, req.Id);
            return result.Select(x => _mapper.Map(x)).ToList();
        }

        public async Task<IEnumerable<TimeEntryResponse>> GetPreceptorTimeEntriesAsync(PreceptorTimeEntryRequest req)
        {
            var result = await _repo.GetTeacherAsync(req.Year, req.Id);
            return result.Select(x => _mapper.Map(x)).ToList();
        }

        public async Task<TimeEntryResponse> GetTimeEntryAsync(GetTimeEntryRequest req)
        {
            var result = await _repo.GetEntryAsync(req.Id);
            return _mapper.Map(result);
        }
    }
}
