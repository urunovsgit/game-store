using game_store_domain.Entities;

namespace game_store_business.Models
{
    public class GenreNodeModel
    {
        public int Id { get; set; }
        public Genre Genre { get; set; }
        public int? ParentId { get; set; }
        public ICollection<GenreNodeModel> SubGenres { get; set; }
    }
}
