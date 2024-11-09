using Microsoft.EntityFrameworkCore;
using SmartCarAPI.Models;

namespace SmartCarAPI.Data
{
    public class ApplicationDbContext :  DbContext
    {
        public ApplicationDbContext () { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Car> Car {  get; set; }
        public DbSet<Damage> Damage { get; set; }
        public DbSet<Severity> Severity { get; set; }
        public DbSet<CarSeverity> CarSeverity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring relationships between CarSeverity, Damage, and Severity
            modelBuilder.Entity<CarSeverity>()
                .HasOne(cs => cs.Damage)
                .WithMany()  // Each Damage can have multiple CarSeverities
                .HasForeignKey(cs => cs.DamageId);

            modelBuilder.Entity<CarSeverity>()
                .HasOne(cs => cs.Severity)
                .WithMany()  // Each Severity can have multiple CarSeverities
                .HasForeignKey(cs => cs.SeverityId);

            modelBuilder.Entity<CarSeverity>()
                .HasOne(cs => cs.Car)
                .WithMany()  // Each Severity can have multiple CarSeverities
                .HasForeignKey(cs => cs.CarId);
        }

    }
}
