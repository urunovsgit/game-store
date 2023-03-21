using game_store.Infrastructure;
using game_store_business.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;


namespace game_store.Models
{
    public class GamesListViewModel
    {
        public GamesListViewModel(IEnumerable<GameModel> games,
                                  IEnumerable<GenreNodeModel> allGenreNodes,
                                  GamesFilterOptions filterOptions)
        {
            Games = new List<SingleGameViewModel>();
            games.ToList().ForEach(game => Games.Add(new SingleGameViewModel(game)));

            AllGenreNodes = allGenreNodes.ToList();
            FilterOptions = filterOptions;
        }

        public List<SingleGameViewModel> Games { get; set; }
        public List<GenreNodeModel> AllGenreNodes { get; set; }
        public GamesFilterOptions FilterOptions { get; set; }
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
                                GenreName = sgenre.Genre.GetAttribute<DisplayAttribute>().Name,
                                Checked = FilterOptions.AppliedGenresIds?.Contains(sgenre.Id) ?? false
                            });
                        }
                    }

                    genreModels.Add(new GenreNodeViewModel
                    {
                        GenreNodeId = node.Id,
                        GenreName = node.Genre.GetAttribute<DisplayAttribute>().Name,
                        Checked = FilterOptions.AppliedGenresIds?.Contains(node.Id) ?? false,
                        SubGenreModels = subGenres
                    });
                }

                return genreModels;
            }
        }
    }
}
