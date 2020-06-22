using PreceptorTime.Domain.Mapper;
using PreceptorTime.Domain.Repositories;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreceptorTime.Domain.Services
{
    //TODO: this will probably end up getting replaced with the .net built in middleware
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _repo;
        private readonly IUserMapper _mapper;

        public AuthorizationService(IUserRepository repo, IUserMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserResponse> LogInAsync(LogInRequest req)
        {
            var user = await _repo.GetAsync(req.UserName);
            if (user == null)
                throw new KeyNotFoundException($"Could not find username: {req.UserName}");

            if (user.Password == req.Password)
                return _mapper.Map(user);

            //TODO: Generate TOKEN and update date...
            return null;
        }

        public async Task<bool> LogOutAsync(LogOutRequest req)
        {
            var user = await _repo.GetAsync(req.DisplayName);
            if (user == null)
                throw new KeyNotFoundException($"Could not find username: {req.DisplayName}");

            if (user.Token == req.Token)
            {
                user.Token = string.Empty;
                user.TokenExpiration = null;

                _repo.Update(user);
                await _repo.UnitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<UserResponse> RegisterAsync(RegisterUserRequest req)
        {
            var user = _mapper.Map(req);

            var addedUser = _repo.Add(user);
            await _repo.UnitOfWork.SaveChangesAsync();
            return _mapper.Map(addedUser);
        }
    }
}
