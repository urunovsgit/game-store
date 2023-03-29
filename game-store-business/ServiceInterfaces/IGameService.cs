using game_store_business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_business.ServiceInterfaces
{
    public interface IGameService : ICrudService<GameModel>
    {
        Task<IEnumerable<GameModel>> GetGamesByFilter(GamesFilterOptions options);
        Task<IEnumerable<GenreNodeModel>> GetAllGenreNodesModelsAsync();
    }
}
