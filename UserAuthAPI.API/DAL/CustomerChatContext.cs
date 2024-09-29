using UserAuthAPI.API.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserAuthAPI.API.DAL
{
    public class UserAuthAPIContext : IdentityDbContext<AppUser>
    {
        public UserAuthAPIContext(DbContextOptions<UserAuthAPIContext> options ) : base (options)
        {
        }

        public DbSet<AppUser> User { get; set; }
        public DbSet<Orders> Order{ get; set; }
    }
}
