using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessControl.Domain.Entities;

namespace AccessControl.Application.Persistence
{
    public interface IUserRepository
    {
        Task AddUserAsync(User? user);
        Task UpdateAsync(User? user);
        Task<User> GetByIdAsync(int id);
        Task<User?> GetByUserNameAsync(string userName);

    }
}
