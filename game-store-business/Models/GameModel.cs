using game_store_domain.Entities;

namespace game_store_business.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
        public IEnumerable<CommentModel> CommentModels { get; set; }
    }
}
