using AutoMapper;
using Business;
using Data.Interfaces;
using game_store_business.Models;
using game_store_business.ServiceInterfaces;
using game_store_domain.Data;
using game_store_domain.Entities;

namespace game_store_business.Services
{
    public class GameStoreServiceProvider : IGameStoreServices
    {
        private readonly IGameService _gameServiceProvider;
        private readonly IOrderService _orderServiceProvider;
        private readonly ICommentService _commentServiceProvider;

        public GameStoreServiceProvider(IGameService gameServiceProvider,
                                        IOrderService orderServiceProvider,
                                        ICommentService commentServiceProvider)
        {
            _gameServiceProvider = gameServiceProvider;
            _orderServiceProvider = orderServiceProvider;
            _commentServiceProvider = commentServiceProvider;

            
        }

        public async Task<CommentModel> AddCommentAsync(CommentModel commentDTO)
        {
            return await _commentServiceProvider.CreateAsync(commentDTO);
        }

        public async Task<CartModel> AddGameToCartAsync(int gameId, int cartId)
        {
            return await _orderServiceProvider.AddGameToCartAsync(gameId, cartId);
        }

        public async Task<CartModel> RemoveGameFromCartAsync(int cartId, int itemId)
        {
            return await _orderServiceProvider.RemoveGameFromCartAsync(cartId, itemId);
        }

        public async Task<GameModel> AddNewGameAsync(GameModel gameDTO)
        {
            return await _gameServiceProvider.CreateAsync(gameDTO);
        }

        public async Task ConfirmOrderAsync(OrderModel orderDTO)
        {
            await _orderServiceProvider.CreateAsync(orderDTO);
        }

        public async Task<CartItemUpdateResponse> IncreaseGameQuantityAsync(int cartItemId)
        {
            return await _orderServiceProvider.IncreaseGameQuantityAsync(cartItemId);
        }

        public async Task<CartItemUpdateResponse> DecreaseGameQuantityAsync(int cartItemId)
        {
            return await _orderServiceProvider.DecreaseGameQuantityAsync(cartItemId);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _commentServiceProvider.DeleteByIdAsync(id);
        }

        public async Task DeleteGameAsync(int id)
        {
            await _gameServiceProvider.DeleteByIdAsync(id);
        }

        public async Task<CommentModel> EditCommentAsync(CommentModel commentDTO)
        {
            return await _commentServiceProvider.UpdateAsync(commentDTO);
        }

        public async Task<IEnumerable<GenreNodeModel>> GetAllGenreNodesModelsAsync()
        {
            return await _gameServiceProvider.GetAllGenreNodesModelsAsync();
        }

        public async Task<GameModel> GetGameByIdAsync(int id)
        {
            return await _gameServiceProvider.GetByIdAsync(id);
        }

        public async Task<IEnumerable<GameModel>> GetGamesAsync(GamesFilterOptions options)
        {
            return await _gameServiceProvider.GetGamesByFilter(options);
        }

        public async Task<CommentModel> RestoreCommentAsync(int id)
        {
            return await _commentServiceProvider.RestoreCommentAsync(id);
        }

        public async Task<GameModel> UpdateGame(GameModel gameDTO)
        {
            return await _gameServiceProvider.UpdateAsync(gameDTO);
        }
    }
}
