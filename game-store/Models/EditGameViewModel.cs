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
            AllGenreNodes = allGenreNodes;
        }

        public IEnumerable<GenreNodeModel> AllGenreNodes { get; set; } = new List<GenreNodeModel>();

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

        public IEnumerable<GenreNodeViewModel> GenreModels
        {
            get
            {
                var genreModels = new List<GenreNodeViewModel>();
                var parrentNodes = AllGenreNodes.Where(gnm => gnm.ParentId == null);

                foreach (var node in parrentNodes)
                {
                    var subGenres = new Collection<GenreNodeViewModel>();

                    if (node.SubGenres.Any())
                    {
                        foreach (var sgenre in node.SubGenres)
                        {
                            subGenres.Add(new GenreNodeViewModel
                            {
                                GenreNodeId = sgenre.Id,
                                Genre = sgenre.Genre,
                                GenreName = sgenre.Genre.GetAttribute<DisplayAttribute>().Name,
                                Checked = Genres.Any(g => g == sgenre.Genre)
                            });
                        }
                    }

                    genreModels.Add(new GenreNodeViewModel
                    {
                        GenreNodeId = node.Id,
                        Genre = node.Genre,
                        GenreName = node.Genre.GetAttribute<DisplayAttribute>().Name,
                        Checked = Genres.Any(g => g == node.Genre),
                        SubGenreModels = subGenres
                    });
                }

                return genreModels;
            }
        }
    }
}
