using Microsoft.EntityFrameworkCore;
using CarService.Models;

namespace CarService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Reservation>()
                .HasOne(p => p.Car)
                .WithMany(p => p.Reservation)
                .HasForeignKey(p => p.Id);

            modelBuilder
                .Entity<Car>()
                .HasMany(p => p.Reservation)
                .WithOne(p => p.Car!)
                .HasForeignKey(p => p.Id);
        }
    }
}