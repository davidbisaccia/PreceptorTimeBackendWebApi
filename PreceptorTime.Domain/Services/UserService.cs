using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Mapper;
using PreceptorTime.Domain.Repositories;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreceptorTime.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserInfoMapper _mapper;
        private readonly IUserRepository _repo;
        private readonly List<AccountType> _learnerTypes = new List<AccountType>() { AccountType.Resident, AccountType.Student };
        private readonly List<AccountType> _preceptorTypes = new List<AccountType>() { AccountType.Preceptor, AccountType.Admin };

        public UserService(IUserInfoMapper mapper, IUserRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IEnumerable<UserInfoResponse>> GetLearnersAsync()
        {
            var results = await _repo.GetAsync(_learnerTypes);
            return results.Select(x => _mapper.Map(x)).ToList();
        }

        public async Task<IEnumerable<UserInfoResponse>> GetPreceptorsAsync()
        {
            var results = await _repo.GetAsync(_preceptorTypes);
            return results.Select(x => _mapper.Map(x)).ToList();
        }

        public async Task<IEnumerable<UserInfoResponse>> GetUsersAsync()
        {
            var results = await _repo.GetAsync();
            return results.Select(x => _mapper.Map(x)).ToList();
        }

        public async Task<bool> ResetUserPasswordAsync(ResetPasswordRequest req)
        {
            var user = await _repo.GetAsync(req.Id);
            if (user == null)
                throw new KeyNotFoundException($"Could not find user with id: {req.Id}");

            user.Password = req.Password;
            var updatedUser = _repo.Update(user);
            if (updatedUser == null)
                return false;

            await _repo.UnitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserAccountStatus(UpdateAccountStatusRequest req)
        {
            var user = await _repo.GetAsync(req.Id);
            if (user == null)
                throw new KeyNotFoundException($"Could not find user with id: {req.Id}");

            user.Active = req.Active;
            var updatedUser = _repo.Update(user);
            if (updatedUser == null)
                return false;

            await _repo.UnitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
