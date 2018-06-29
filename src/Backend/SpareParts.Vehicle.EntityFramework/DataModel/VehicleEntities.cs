using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SpareParts.Vehicle.DomainModel;

namespace SpareParts.Vehicle.EntityFramework.DataModel
{
    internal class VehicleEntities : DbContext
    {
        public VehicleEntities(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Vehicle");
            modelBuilder.Entity<DomainModel.Vehicle>().HasKey(m => m.Id);
            modelBuilder.Entity<DomainModel.Vehicle>().Property(m => m.Id).HasMaxLength(36);
            modelBuilder.Entity<DomainModel.Vehicle>().Property(m => m.Brand).HasMaxLength(255);
            modelBuilder.Entity<DomainModel.Vehicle>().Property(m => m.Model).HasMaxLength(255);
            modelBuilder.Entity<DomainModel.Vehicle>().Property(m => m.Customer).HasMaxLength(255);
            modelBuilder.Entity<DomainModel.Vehicle>().Property(m => m.Color).HasMaxLength(20);
            modelBuilder.Entity<DomainModel.Vehicle>().Property(m => m.Plate).HasMaxLength(20);
        }

        public DbSet<DomainModel.Vehicle> Vehicles { get; set; }
    }
}
