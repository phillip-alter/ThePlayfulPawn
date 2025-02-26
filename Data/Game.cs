using Microsoft.EntityFrameworkCore;

namespace ThePlayfulPawn.Data 
{
    public class Game
    {
        public int GameId { get; set; }
        public int VendorId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int MaxPlayerCount { get; set; }
    }
}