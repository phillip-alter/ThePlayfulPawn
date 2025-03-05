using ThePlayfulPawn.Data;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace ThePlayfulPawn.Models
{
    public class AdminModel
    {
        public IEnumerable<Customer> Customers { get; set; } = new List<Customer>();
        public IEnumerable<Address> Addresses { get; set; } = new List<Address>();
        public IEnumerable<Vendor> Vendors { get; set; } = new List<Vendor>();
        public IEnumerable<Food> Foods { get; set; } = new List<Food>();

        private PawnDbContext _context {get; set;}


        //context is the database(CreateThePlayfulPawn.sql) that we are pulling the information from about the customer, addresses, vendors, and food.
        public AdminModel(PawnDbContext context)
        {
            _context = context;
            Customers = _context.Customers.ToList();
            Addresses = _context.Addresses.ToList();
            Vendors = _context.Vendors.ToList();
            
        }
    }
}