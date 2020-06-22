using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreceptorTime.Domain.Services
{
    //TODO: this will probably end up getting replaced with the .net built in middleware
    public interface IAuthorizationService
    {
        Task<UserResponse> LogInAsync(LogInRequest req);
        Task<UserResponse> RegisterAsync(RegisterUserRequest req);

        Task<bool> LogOutAsync(LogOutRequest req);
    }
}
