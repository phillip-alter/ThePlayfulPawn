using Microsoft.EntityFrameworkCore;

namespace ThePlayfulPawn.Data
{
    public class PawnDbContext : DbContext
    {
        public PawnDbContext(DbContextOptions<PawnDbContext> options) : base(options){}
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Vendors> Vendors { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
    }
}