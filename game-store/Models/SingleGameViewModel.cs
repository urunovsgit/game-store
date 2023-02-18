using game_store_domain.Entities;

namespace game_store.Models
{
    public class SingleGameViewModel : Game
    {
        public List<GenreNode> GenreNodes { get; set; }
        public IFormFile UploadedImage
        {
            get
            {
                if (Image == null) return null;

                var memStream = new MemoryStream(Image);

                return new FormFile(memStream, 0, memStream.Length, "img", "img.jpeg");
            }
            set
            {
                if (value != null)
                {
                    var memStream = new MemoryStream();

                    value.CopyTo(memStream);
                    Image = memStream.ToArray();

                    memStream.Close();
                    memStream.Dispose();
                }
            }
        }

        public SingleGameViewModel()
        {

        }

        public SingleGameViewModel(Game game, List<GenreNode> allGenres)
        {
            Id = game.Id;
            CopyFrom(game);
            GenreNodes = allGenres;
        }
    }
}
