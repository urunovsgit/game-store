using game_store_domain.Entities;

namespace game_store_business.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public ICollection<int> CommentsIds { get; set; }
    }
}
