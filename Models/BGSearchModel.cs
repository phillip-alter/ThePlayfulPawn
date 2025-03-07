using ThePlayfulPawn.Data;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace ThePlayfulPawn.Models
{
        public class BGSearchModel
        {
            public string? InputBGName { get; set; } = string.Empty;
            public int? InputPlayerCount { get; set; }
            private PawnDbContext _context { get; }
            public IEnumerable<Game> Games { get; set; } = new List<Game>();
            public IEnumerable<Vendor> Vendors { get; set; } = new List<Vendor>();
    }
}