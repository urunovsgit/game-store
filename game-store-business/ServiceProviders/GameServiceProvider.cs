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
                games = FilterByGenres(games, options.AppliedGenres);
            }

            if (options.TitleSubstring != null)
            {
                games = FilterByTitle(games, options.TitleSubstring);
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

        private IEnumerable<Game> FilterByGenres(IEnumerable<Game> games, ICollection<int> appliedGenres)
        {
            var genresNodes = _gsUnitOfWork.GenreNodeRepository.GetAllAsync().Result;
            var genres = genresNodes.Where(gn => appliedGenres.Contains((int)gn.Genre))
                                                                .Select(gn => gn.Genre);

            return games.Where(game => game.Genres
                             .Any(genre => genres
                                .Contains(genre)));
        }

        private IEnumerable<Game> FilterByTitle(IEnumerable<Game> games, string entry)
        {
            return games.Where(game => game.Title
                            .Contains(entry, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
