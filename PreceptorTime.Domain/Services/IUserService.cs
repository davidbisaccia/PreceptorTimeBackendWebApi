using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreceptorTime.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfoResponse>> GetUsersAsync();
        Task<IEnumerable<UserInfoResponse>> GetPreceptorsAsync();
        Task<IEnumerable<UserInfoResponse>> GetLearnersAsync();

        Task<bool> ResetUserPasswordAsync(ResetPasswordRequest req);
        Task<bool> UpdateUserAccountStatus(UpdateAccountStatusRequest req);
    }
}
