using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Domain.Repositories
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
