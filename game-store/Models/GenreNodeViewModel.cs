using game_store_domain.Entities;

namespace game_store.Models
{
    public class GenreNodeViewModel
    {
        public int GenreNodeId { get; set; }
        public Genre Genre { get; set; }
        public string GenreName { get; set; }
        public bool Checked { get; set; }
        public ICollection<GenreNodeViewModel> SubGenreModels { get; set; }
    }
}
