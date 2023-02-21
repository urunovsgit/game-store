using game_store_domain.Entities;
using game_store_domain.Services.Infrastrucure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Services
{
    public interface IGameServices
    {
        public (IEnumerable<Game>, int) GetGames(FilterPageOptions options);
        public IEnumerable<Game> GetAllGames();
        public IEnumerable<Game> GetGamesByGenres(IEnumerable<Genre> genres);
        public IEnumerable<Game> GetGamesByTitle(string title);
        public Game GetGameById(int id);
        public Game AddNewGame(Game game);
        public Game UpdateGame(Game game);
        public void DeleteGame(int id);
        public List<GenreNode> GetAllGenreNodes();
    }
}
