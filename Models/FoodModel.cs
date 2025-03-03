using ThePlayfulPawn.Data;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace ThePlayfulPawn.Models
{
    public class FoodModel
    {
        public int FoodId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Inventory { get; set; }
    }
}