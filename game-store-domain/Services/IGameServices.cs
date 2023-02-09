using game_store_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Services
{
    public interface IGameServices
    {
        public (IEnumerable<Game>, int) GetGames(List<Genre> genres, int pageSize, int page);
        public Game GetGameById(int id);
        public Game AddNewGame(Game game);
        public Game UpdateGame(Game game);
        public void DeleteGame(int id);
    }
}
