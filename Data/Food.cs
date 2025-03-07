using Microsoft.EntityFrameworkCore;

namespace ThePlayfulPawn.Data 
{
    public class Food
    {
        public int FoodId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Inventory { get; set; }

    }
}