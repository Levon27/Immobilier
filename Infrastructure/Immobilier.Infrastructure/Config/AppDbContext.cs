using Immobilier.Domain;
using Microsoft.EntityFrameworkCore;

namespace Immobilier.Infrastructure.Config
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
    }
}
