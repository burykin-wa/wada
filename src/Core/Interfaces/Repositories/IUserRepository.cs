using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUserByNameAsync(string name);

        Task<long> InsertUserAsync(UserEntity userEntity);
    }
}
