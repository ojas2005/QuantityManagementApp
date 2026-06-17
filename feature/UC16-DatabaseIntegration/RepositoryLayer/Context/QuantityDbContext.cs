using Microsoft.EntityFrameworkCore;
using ModelLayer.Entities;

namespace RepositoryLayer.Context
{
    public class QuantityDbContext : DbContext
    {
        public QuantityDbContext(DbContextOptions<QuantityDbContext> options)
            : base(options)
        {
        }

        public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; }

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
                
                // Create indexes for better query performance
                entity.HasIndex(e => e.OperationType);
                entity.HasIndex(e => e.FirstMeasurementType);
                entity.HasIndex(e => e.Timestamp);
            });
        }
    }
}