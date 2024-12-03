using Microsoft.EntityFrameworkCore;
using AccessControl.Domain.Entities;

namespace AccessControl.Infrastructure
{
    public class AccessControlContext:DbContext
    {
        public DbSet<User?> Users { get; set; }

        public AccessControlContext(DbContextOptions<AccessControlContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }
}
