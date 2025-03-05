using Microsoft.EntityFrameworkCore;

namespace ThePlayfulPawn.Data 
{
    public class Vendor
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; } = string.Empty;
        public int VendorAddressID { get; set; }
        public string ContactFirst { get; set; } = string.Empty;
        public string ContactLast { get; set; } = string.Empty;
        public Address? Address { get; set; } // Add this line (FOR CRUD LINKING)

    }
}