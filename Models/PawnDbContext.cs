using Microsoft.EntityFrameworkCore;

namespace ThePlayfulPawn.Data
{
    public class PawnDbContext : DbContext
    {
        public PawnDbContext(DbContextOptions<PawnDbContext> options) : base(options){}
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Address> Addresses { get; set; }



        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //This configuration tells EF Core to automatically delete the related Address record when a Customer record is deleted.
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Address)
                .WithMany()
                .HasForeignKey(c => c.AddressId)
                .OnDelete(DeleteBehavior.Cascade);

            //This configuration tells EF Core to automatically delete the related Address record when a Vendor record is deleted.
            modelBuilder.Entity<Vendor>()
                .HasOne(v => v.Address)
                .WithMany()
                .HasForeignKey(v => v.VendorAddressID) // Corrected here
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}