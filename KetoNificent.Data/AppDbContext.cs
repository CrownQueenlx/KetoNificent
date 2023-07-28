using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Data.Entities;

public partial class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public override DbSet<UserEntity> Users { get; set; } = null!;

    public virtual DbSet<IngredientEntity> Ingredients { get; set; } = null!;

    public virtual DbSet<ProductEntity> Products { get; set; } = null!;

    public virtual DbSet<ServingEntity> Servings { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:KetoNificentMVC");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>().ToTable("UserEntity").HasKey(u => u.Id);

        modelBuilder.Entity<RoleEntity>().ToTable("Roles");

        modelBuilder.Entity<IngredientEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingreden__3214EC07C413A14C");

            entity.ToTable("IngredientEntity", "Keto");

            entity.Property(e => e.DefaultMeasurement)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NCarbCt).HasColumnName("NCarbCt");
        });

        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07A4D7E36A");

            entity.ToTable("ProductEntity", "Keto");

            entity.Property(e => e.Name)
                .HasMaxLength(1)
                .IsUnicode(false);
        entity.Property(e => e.Name)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.ProductEntities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductEntity_UserEntity");
        });

        modelBuilder.Entity<ServingEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Serving__3214EC073158665D");

            entity.ToTable("ServingEntity", "Keto");

            entity.Property(e => e.Measurement)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.Ingredent).WithMany(p => p.Servings)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK__Serving__Ingrede__3F466844");

            entity.HasOne(d => d.Product).WithMany(p => p.Servings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Serving__Product__403A8C7D");
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserEnti__1788CC4C272CD311");

            entity.ToTable("UserEntity", "Keto");

            entity.Property(e => e.Name)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
