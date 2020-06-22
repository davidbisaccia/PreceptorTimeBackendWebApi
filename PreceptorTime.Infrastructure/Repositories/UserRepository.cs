using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PreceptorTime.Domain.Entities;
using PreceptorTime.Domain.Repositories;

namespace PreceptorTime.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PreceptorTimeContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public UserRepository(PreceptorTimeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User Add(User user)
        {
            return _context.Users
                .Add(user)
                .Entity;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAsync(List<AccountType> accountTypes)
        {
            return await _context.Users
                .Where(u => accountTypes.Contains(u.Account))
                .Select(u => u)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetAsync(string displayName)
        {
            return await _context.Users
                .Where(u => u.DisplayName == displayName)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public User Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return user;
        }
    }
}
