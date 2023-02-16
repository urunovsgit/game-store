using game_store_domain.Entities;
using game_store_domain.Services.Infrastrucure;

namespace game_store.Models
{
    public class GamesViewModel
    {
        public SortFilterPageOptions Options { get; set; }
        public IEnumerable<Game> Games { get; set; }
        public int TotalGamesCount { get; set; }
    }
}
