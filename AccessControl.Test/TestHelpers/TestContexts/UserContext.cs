using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessControl.Domain.Entities;
using AccessControl.Infrastructure;

namespace AccessControl.Test.TestHelpers.TestContexts
{
    public class UserContext : BaseTestContext
    {
        private AccessControlContext _context;
        public UserContext(AccessControlContext context) : base(context)
        {
            _context = context;
        }

        public User DefaultUser = new User
        {
            Id = GetDefaultId(),
            UserName = GetUserName(),
            DisplayName = GetUserFullName(),
            WindowsId = GetRandomString(10),
            Email = GetEmailAddress()
        };
        public async Task<User> CreateDefaultUser(int? id = null, string? userName = null)
        {
            var user = new User
            {
                Id = id ?? GetDefaultId(),
                UserName = userName ?? GetUserName(),
                DisplayName = GetUserFullName(),
                WindowsId = GetRandomString(10),
                Email = GetEmailAddress()
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
