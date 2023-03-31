using game_store.Infrastructure;
using game_store_business.Models;
using game_store_domain.Entities;
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
            var genres = filterOptions.AppliedGenres?.Select(g => (Genre)g);
            games.ToList().ForEach(game => Games.Add(new SingleGameViewModel(game)));

            GenreModels = allGenreNodes.ToGenreNodeViewModels(genres ?? allGenreNodes.Select(gn => gn.Genre));
            FilterOptions = filterOptions;
        }

        public List<SingleGameViewModel> Games { get; set; }
        public GamesFilterOptions FilterOptions { get; set; }
        public IEnumerable<GenreNodeViewModel> GenreModels { get; private set; }
    }
}
