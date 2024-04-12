using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSet properties represent database tables
    public DbSet<Users> Users { get; set; }
    public DbSet<Posts> Posts { get; set; }

    public DbSet<Comments> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure one-to-many relationship between Users and Posts
        modelBuilder.Entity<Users>()
            .HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Example: Cascade delete if a user is deleted

        // Configure one-to-many relationship between Users and Comments
        modelBuilder.Entity<Users>()
            .HasMany(u => u.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Example: Cascade delete if a user is deleted

        // Configure one-to-many relationship between Posts and Comments
        modelBuilder.Entity<Posts>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade); // Example: Cascade delete if a post is deleted
    }

}