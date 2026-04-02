using Microsoft.EntityFrameworkCore;
using ModelLayer.Entities;
using ModelLayer.Models;

namespace RepositoryLayer.Context;

public class QuantityDbContext : DbContext
{
    public QuantityDbContext(DbContextOptions<QuantityDbContext> options)
        : base(options)
    {
    }

    public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure QuantityMeasurementEntity
        modelBuilder.Entity<QuantityMeasurementEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OperationType).IsRequired().HasMaxLength(20);
            entity.Property(e => e.FirstUnit).HasMaxLength(10);
            entity.Property(e => e.FirstMeasurementType).HasMaxLength(20);
            entity.Property(e => e.SecondUnit).HasMaxLength(10);
            entity.Property(e => e.SecondMeasurementType).HasMaxLength(20);
            entity.Property(e => e.ResultUnit).HasMaxLength(10);
            entity.Property(e => e.ResultMeasurementType).HasMaxLength(20);
            entity.Property(e => e.ErrorMessage).HasMaxLength(500);
            
            entity.HasIndex(e => e.OperationType);
            entity.HasIndex(e => e.FirstMeasurementType);
            entity.HasIndex(e => e.Timestamp);
        });

        // Configure User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.GoogleId);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(200);
            entity.Property(e => e.GoogleId).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(20).HasDefaultValue("User");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.ProfilePicture).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });
    }
}
