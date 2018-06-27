using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SpareParts.Part.EntityFramework.DataModel
{
    internal class PartEntities : DbContext
    {
        public PartEntities(DbContextOptions options) : base(options)
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

            modelBuilder.HasDefaultSchema("Part");
            modelBuilder.Entity<DomainModel.Part>().HasKey(m => m.Code);
            modelBuilder.Entity<DomainModel.Part>().Property(m => m.Code).HasMaxLength(255);
            modelBuilder.Entity<DomainModel.Part>().Property(m => m.Name).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<DomainModel.Part>().Property(m => m.PhotoUri).HasMaxLength(1000);
        }

        public DbSet<DomainModel.Part> Parts { get; set; }
    }
}
