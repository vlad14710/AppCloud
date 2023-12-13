using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace AppCloud
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<FileModel> Files { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
