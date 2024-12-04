using Microsoft.EntityFrameworkCore;
using test2.Model;

namespace test2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        // Define the DbSet for your NameModel
        public DbSet<NameModel> Names { get; set; }
    }
}
