using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SpareParts.Vehicle.DomainModel;

namespace SpareParts.Vehicle.EntityFramework.DataModel
{
    internal class CatalogEntities : DbContext
    {
        public CatalogEntities(DbContextOptions options) : base(options)
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

            modelBuilder.HasDefaultSchema("Catalog");
            modelBuilder.Entity<Movie>().HasKey(m => m.Id);
            modelBuilder.Entity<Movie>().Property(m => m.Id).HasMaxLength(36);
            modelBuilder.Entity<Movie>().Property(m => m.Title).HasMaxLength(255);
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
