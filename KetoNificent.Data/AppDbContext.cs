using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Data.Entities;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<UserEntity> Users { get; set; } = null!;

    public virtual DbSet<IngredientEntity> Ingredients { get; set; } = null!;

    public virtual DbSet<ProductEntity> Products { get; set; } = null!;

    public virtual DbSet<ServingEntity> Servings { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=KetoNificent;User=sa;Password=April28Free!;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>().ToTable("Users");

        modelBuilder.Entity<RoleEntity>().ToTable("Roles");
        modelBuilder.Entity<UserRoleEntity>().ToTable("UserClaims");
        modelBuilder.Entity<UserClaimEntity>().ToTable("UserLogins");
        modelBuilder.Entity<UserTokenEntity>().ToTable("UserTokens");
        modelBuilder.Entity<RoleClaimEntity>().ToTable("RolesClaims");

        modelBuilder.Entity<IngredientEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingreden__3214EC07C413A14C");

            entity.ToTable("Ingredient", "Keto");

            entity.Property(e => e.DefaultMeasurement)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NCarbCt).HasColumnName("NCarb");
        });

        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07A4D7E36A");

            entity.ToTable("Product", "Keto");

            entity.Property(e => e.Name)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ServingEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Serving__3214EC073158665D");

            entity.ToTable("Serving", "Keto");

            entity.Property(e => e.Measurement)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.Ingredent).WithMany(p => p.Servings)
                .HasForeignKey(d => d.IngredentId)
                .HasConstraintName("FK__Serving__Ingrede__3F466844");

            entity.HasOne(d => d.Product).WithMany(p => p.Servings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Serving__Product__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);

         // modelBuilder.Entity<ProductEntity>()
            // .HasOne(n => n.UserId)
            // .WithMany(p => p.UserId) 
            // .HasForeignKey(nameof => nameof.UserId);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
