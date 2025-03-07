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
    }
}