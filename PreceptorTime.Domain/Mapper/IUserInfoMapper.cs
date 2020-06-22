using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Mapper
{
    public interface IUserInfoMapper
    {
        UserInfoResponse Map(User user);
    }
}
