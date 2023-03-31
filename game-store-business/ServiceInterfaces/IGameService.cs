using game_store_business.Models;

namespace game_store_business.ServiceInterfaces
{
    public interface IGameService : ICrudService<GameModel>
    {
        Task<IEnumerable<GameModel>> GetGamesByFilter(GamesFilterOptions options);
        Task<IEnumerable<GenreNodeModel>> GetAllGenreNodesModelsAsync();
    }
}
