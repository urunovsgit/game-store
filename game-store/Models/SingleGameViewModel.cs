using game_store_domain.Entities;

namespace game_store.Models
{
    public class SingleGameViewModel
    {
        public Game Game { get; set; }
        public List<GenreNode> GenreNodes { get; set; }
    }
}
