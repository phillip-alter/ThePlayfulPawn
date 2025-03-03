using ThePlayfulPawn.Data;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace ThePlayfulPawn.Models
{
    public class VendorModel
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; } = string.Empty;
        public string VendorAddressID { get; set; } = string.Empty;
        public string ContactFirst { get; set; } = string.Empty;
        public string ContactLast { get; set; } = string.Empty;
    }
}