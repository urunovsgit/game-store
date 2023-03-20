using game_store_domain.Entities;

namespace game_store.Models
{
    public class GamesViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public string TitleKey { get; set; } = "";
    }
}
