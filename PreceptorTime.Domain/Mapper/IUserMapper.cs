using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Requests;
using PreceptorTime.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Mapper
{
    public interface IUserMapper
    {
        User Map(RegisterUserRequest req);
        UserResponse Map(User user);
    }
}
