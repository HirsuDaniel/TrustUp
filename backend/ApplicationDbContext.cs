using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet properties represent database tables
    public DbSet<Users> Users { get; set; }
    public DbSet<Posts> Posts { get; set; }

}