using game_store.Infrastructure;
using game_store_business.Models;
using game_store_domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace game_store.Models
{
    public class EditGameViewModel : GameViewModelBase
    {
        public EditGameViewModel() { }
        public EditGameViewModel(GameModel model, IEnumerable<GenreNodeModel> allGenreNodes) : base(model)
        {
            GenreModels = allGenreNodes.ToGenreNodeViewModels(model.Genres);
        }


        private IFormFile? _uploadedImage;
        public IFormFile? UploadedImage
        {
            get
            {
                return _uploadedImage;
            }
            set
            {
                if (value != null)
                {
                    _uploadedImage = value;
                    var memStream = new MemoryStream();

                    value.CopyTo(memStream);
                    Image = memStream.ToArray();

                    memStream.Close();
                    memStream.Dispose();
                }
            }
        }
        public IEnumerable<GenreNodeViewModel> GenreModels { get; private set; }
    }
}
