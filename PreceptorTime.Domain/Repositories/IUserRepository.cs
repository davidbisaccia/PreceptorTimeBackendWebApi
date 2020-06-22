using PreceptorTime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreceptorTime.Domain.Repositories
{
    public interface IUserRepository : IRepository
	{
		Task<IEnumerable<User>> GetAsync();
		Task<IEnumerable<User>> GetAsync(List<AccountType> accountTypes);
		Task<User> GetAsync(string displayName);
		Task<User> GetAsync(int id);
		User Add(User user);
		User Update(User user);
	}
}
