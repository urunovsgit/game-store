using game_store_business.Models;
using game_store_business.ServiceInterfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_business.ServiceProviders
{
    public class CachingGameServiceProvider : IGameService
    {
        private readonly string _gamesCacheKey = "Games";
        private readonly IMemoryCache _cache;
        private readonly IGameService _gameService;

        public CachingGameServiceProvider(IGameService gameService, IMemoryCache cache)
        {
            _gameService = gameService;
            _cache = cache;
        }

        public async Task<GameModel> CreateAsync(GameModel modelDTO)
        {
            _cache.Remove(_gamesCacheKey);
            return await _gameService.CreateAsync(modelDTO);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _cache.Remove(_gamesCacheKey);
            await _gameService.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<GameModel>> GetAllAsync()
        {
            return await _cache.GetOrCreateAsync(_gamesCacheKey,
                (entry) => _gameService.GetAllAsync());
        }

        public async Task<IEnumerable<GenreNodeModel>> GetAllGenreNodesModelsAsync()
        {
            return await _gameService.GetAllGenreNodesModelsAsync();
        }

        public async Task<GameModel> GetByIdAsync(int id)
        {
            return await _gameService.GetByIdAsync(id);
        }

        public async Task<IEnumerable<GameModel>> GetGamesByFilter(GamesFilterOptions options)
        {
            return await _cache.GetOrCreateAsync(_gamesCacheKey,
                (entry) => _gameService.GetGamesByFilter(options));
        }

        public async Task<GameModel> UpdateAsync(GameModel modelDTO)
        {
            return await _gameService.UpdateAsync(modelDTO);
        }
    }
}
