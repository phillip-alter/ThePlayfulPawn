using ThePlayfulPawn.Data;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace ThePlayfulPawn.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int AddressId { get; set; }
    }
}