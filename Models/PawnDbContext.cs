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
    }
}