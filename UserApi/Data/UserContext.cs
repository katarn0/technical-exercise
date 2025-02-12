namespace WebApi.UserApi.Data;

using Microsoft.EntityFrameworkCore;

using Models;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(128);
            entity.Property(e => e.LastName).HasMaxLength(128);
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(10);
            entity.Property(e => e.DateOfBirth).IsRequired();

            // Ensure email uniqueness
            entity.HasIndex(e => e.Email).IsUnique();
        });
    }
}
