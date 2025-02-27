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

            public BGSearchModel(string? inputBGName, int? inputPlayerCount, PawnDbContext context)
            {
                InputBGName = inputBGName;
                InputPlayerCount = inputPlayerCount;
                _context = context;
            }

            public void Search()
            {
                var query = _context.Games
                    .Join(
                        _context.Vendors,
                        game => game.VendorId,
                        vendor => vendor.VendorId,
                        (game, vendor) => new { Game = game, VendorName = vendor.VendorName }
                    );
                if (!InputBGName.IsNullOrEmpty())
                {
#pragma warning disable CS8604 // Possible null reference argument.
                query = query.Where(g => g.Game.GameName.Contains(InputBGName));
#pragma warning restore CS8604 // Possible null reference argument.
            }

                if (InputPlayerCount.HasValue)
                {
                    query = query.Where(g => g.Game.MaxPlayerCount <= InputPlayerCount);
                }
                Games = query.Select(x => x.Game).ToList();
                Vendors = query.Select(x => new Vendor { VendorName = x.VendorName, VendorId = x.Game.VendorId }).ToList(); // Include VendorId
            }
    }
}