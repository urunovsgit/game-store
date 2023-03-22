using game_store_business.Models;

namespace game_store_business.ServiceInterfaces
{
    public interface IGameStoreServices
    {
        public Task<IEnumerable<GameModel>> GetGamesAsync(GamesFilterOptions options);
        public Task<GameModel> GetGameByIdAsync(int id);
        public Task<GameModel> AddNewGameAsync(GameModel gameDTO);
        public Task<GameModel> UpdateGame(GameModel gameDTO);
        public Task DeleteGameAsync(int id);
        public Task<CommentModel> AddCommentAsync(CommentModel commentDTO);
        public Task<CommentModel> EditCommentAsync(CommentModel commentDTO);
        public Task DeleteCommentAsync(int id);
        public Task<CommentModel> RestoreCommentAsync(int id);
        public Task<CartModel> AddGameToCartAsync(int gameId, int cartId);
        public Task<CartModel> RemoveGameFromCartAsync(int cartId, int itemId);
        public Task<CartItemUpdateResponse> IncreaseGameQuantityAsync(int cartItemId);
        public Task<CartItemUpdateResponse> DecreaseGameQuantityAsync(int cartItemId);
        public Task ConfirmOrderAsync(OrderModel orderDTO);
        public Task<IEnumerable<GenreNodeModel>> GetAllGenreNodesModelsAsync();
    }
}
