using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheGrandImperium_Security.Core.Entities;

namespace TheGrandImperium_Security.Core.Persistence
{
    public class SecurityContext: IdentityDbContext<Usuario>
    {
        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
