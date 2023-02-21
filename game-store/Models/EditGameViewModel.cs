using game_store_domain.Entities;

namespace game_store.Models
{
    public class EditGameViewModel : Game
    {
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

        public EditGameViewModel()
        {

        }

        public EditGameViewModel(Game game)
        {
            Id = game.Id;
            CopyFrom(game);
        }
    }
}
