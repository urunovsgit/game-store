using AutoMapper;
using Data.Interfaces;
using game_store_business.Models;
using game_store_business.ServiceInterfaces;
using game_store_domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_business.Services
{
    public class GameServiceProvider : IGameService
    {
        private readonly IUnitOfWork _gsUnitOfWork;
        private readonly IMapper _mapperProfile;

        public GameServiceProvider(IUnitOfWork unitOfWork, IMapper mapperProfile)
        {
            _gsUnitOfWork = unitOfWork;
            _mapperProfile = mapperProfile;
        }

        public async Task<IEnumerable<GameModel>> GetAllAsync()
        {
            var games = await _gsUnitOfWork.GameRepository.GetAllAsync();
            return _mapperProfile.Map<IEnumerable<GameModel>>(games);
        }

        public async Task<IEnumerable<GameModel>> GetGamesByFilter(GamesFilterOptions options)
        {
            var games = await _gsUnitOfWork.GameRepository.GetAllAsync();

            if (options.AppliedGenres != null)
            {
                var genresNodes = await _gsUnitOfWork.GenreNodeRepository.GetAllAsync();
                var genres = genresNodes.Where(gn => options.AppliedGenres.Contains((int)gn.Genre))
                                                                          .Select(gn => gn.Genre);

                games = games.Where(game => game.Genres
                                 .Any(genre => genres
                                    .Contains(genre)))
                             .ToList();
            }

            if (options.TitleSubstring != null)
            {
                games = games.Where(game => game.Title
                                    .Contains(options.TitleSubstring, StringComparison.InvariantCultureIgnoreCase))
                                    .ToList();
            }

            return _mapperProfile.Map<IEnumerable<GameModel>>(games);
        }

        public async Task<GameModel> GetByIdAsync(int id)
        {
            var game = await _gsUnitOfWork.GameRepository.GetByIdAsync(id);
            return _mapperProfile.Map<GameModel>(game);
        }

        public async Task<GameModel> CreateAsync(GameModel modelDTO)
        {
            var game = _mapperProfile.Map<Game>(modelDTO);

            game = await _gsUnitOfWork.GameRepository.AddAsync(game);
            await _gsUnitOfWork.SaveAsync();

            return _mapperProfile.Map<GameModel>(game);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _gsUnitOfWork.GameRepository.DeleteByIdAsync(id);
            await _gsUnitOfWork.SaveAsync();
        }

        public async Task<GameModel> UpdateAsync(GameModel modelDTO)
        {
            var game = _mapperProfile.Map<Game>(modelDTO);

            _gsUnitOfWork.GameRepository.Update(game);
            await _gsUnitOfWork.SaveAsync();

            game = await _gsUnitOfWork.GameRepository.GetByIdAsync(game.Id);
            return _mapperProfile.Map<GameModel>(game);
        }

        public async Task<IEnumerable<GenreNodeModel>> GetAllGenreNodesModelsAsync()
        {
            var nodes = await _gsUnitOfWork.GenreNodeRepository.GetAllAsync();
            return _mapperProfile.Map<IEnumerable<GenreNodeModel>>(nodes);
        }
    }
}
