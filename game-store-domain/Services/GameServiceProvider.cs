using game_store_domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Services
{
    public class GameServiceProvider : IGameServices
    {
        private readonly DbContext _storeDbContext;

        public GameServiceProvider(GameStoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public (IEnumerable<Game>, int) GetGames(SortFilterPageOptions options)
        {
            var gamesQuery = _storeDbContext.Set<Game>().AsQueryable();

            var games = gamesQuery.OrderAndFilterBy(options)
                                  .Paginate(options)
                                  .ToList();

            return (games, gamesQuery.Count());
        }

        public Game GetGameById(int id)
        {
            return _storeDbContext.Set<Game>().Find(id);
        }

        public Game AddNewGame(Game game)
        {
            throw new NotImplementedException();
        }

        public void DeleteGame(int id)
        {
            throw new NotImplementedException();
        }                    

        public Game UpdateGame(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
