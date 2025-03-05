using Microsoft.EntityFrameworkCore;

namespace ThePlayfulPawn.Data 
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public int AddressId { get; set; }
        public Address Address { get; set; } // Add this line (FOR CRUD LINKING)
    }
}