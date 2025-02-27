using ThePlayfulPawn.Data;
using System.Linq;

namespace ThePlayfulPawn.Models
{
    public class BGSearchModel
    {
        public string? InputBGName { get; set; }
        // public string? InputBGVendor { get; set; }
        public int? InputPlayerCount { get; set; }
        private PawnDbContext _context { get; }
        public IEnumerable<Game> Games { get; set; } = new List<Game>();

        public BGSearchModel(string? inputBGName, int? inputPlayerCount, PawnDbContext context)
        {
            InputBGName = inputBGName;
            //InputBGVendor = inputBGVendor;
            InputPlayerCount = inputPlayerCount;
            _context = context;
        }
        
        public void searchGameName(string? searchTerm){
            List<Game> games = new List<Game>();
            if (string.IsNullOrEmpty(searchTerm)){
                games = _context.Games.ToList();
            } else {
                games = _context.Games.Where(g => g.GameName.Contains(searchTerm)).ToList();
            }
            Games = games;
        }

        public void searchGamePlayers (int? searchTerm){
            List<Game> games = new List<Game>();
            if (searchTerm == null){
                games = _context.Games.ToList();
            } else {
                games = _context.Games.Where(g => g.MaxPlayerCount <= searchTerm).ToList();
            }
            Games = games;
        }
        public void searchNamePlayers (int? searchTerm1, string? searchTerm2){
            List<Game> games = new List<Game>();
            games = _context.Games.Where(g => g.MaxPlayerCount <= searchTerm1).ToList();
#pragma warning disable CS8604 // Possible null reference argument.
            games = games.Where(g => g.GameName.Contains(searchTerm2)).ToList();
#pragma warning restore CS8604 // Possible null reference argument.
            Games = games;
        }
    }
}