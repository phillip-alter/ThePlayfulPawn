using Microsoft.EntityFrameworkCore;

namespace ThePlayfulPawn.Data 
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Line1 { get; set; } = string.Empty;
        public string Line2 { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int ZipCode { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}