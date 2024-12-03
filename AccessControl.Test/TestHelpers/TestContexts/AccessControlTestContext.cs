using AccessControl.Domain.Entities;
using AccessControl.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Test.TestHelpers.TestContexts
{
    public class AccessControlTestContext
    {
        private readonly AccessControlContext _context;
        private UserContext _userContext;
        public AccessControlTestContext()
        {
            var options = new DbContextOptionsBuilder<AccessControlContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AccessControlContext(options);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public AccessControlContext Context => _context;
        public UserContext User => _userContext ??= new UserContext(_context);

    }
}
