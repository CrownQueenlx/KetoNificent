using KetoNificent.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Data;

public class AppDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) { }

    public override DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<ProductEntity> Products { get; set; } = null!;
    public DbSet<IngredientEntity> Ingredients { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>().ToTable("Users");

        modelBuilder.Entity<RoleEntity>().ToTable("Roles");
        modelBuilder.Entity<UserRoleEntity>().ToTable("UserClaims");
        modelBuilder.Entity<UserClaimEntity>().ToTable("UserLogins");
        modelBuilder.Entity<UserTokenEntity>().ToTable("UserTokens");
        modelBuilder.Entity<RoleClaimEntity>().ToTable("RolesClaims");

        // modelBuilder.Entity<ProductEntity>()
            // .HasOne(n => n.UserId)
            // .WithMany(p => p.UserId) 
            // .HasForeignKey(nameof => nameof.UserId);
    }
}