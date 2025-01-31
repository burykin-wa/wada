using Core.Interfaces.Repositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly WadaDbContext _context;

        public UserRepository(WadaDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetUserByNameAsync(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task<long> InsertUserAsync(UserEntity userEntity)
        {
            var user = await _context.Users.AddAsync(userEntity);
            _context.SaveChanges();
            return user.Entity.Id;
        }
    }
}
