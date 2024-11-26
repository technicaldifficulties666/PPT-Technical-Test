using Microsoft.EntityFrameworkCore;
using AvatarApp.Models;

namespace AvatarApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Image> Images { get; set; }
    }
}
