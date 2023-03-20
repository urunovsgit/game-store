using AutoMapper;
using Data.Interfaces;
using game_store_business.Models;
using game_store_domain.Entities;

namespace game_store_domain.Services
{
    public class GameStoreServiceProvider : IGameStoreServices
    {
        private readonly IUnitOfWork _gsUnitOfWork;
        private readonly IMapper _mapperProfile;

        public GameStoreServiceProvider(IUnitOfWork unitOfWork, IMapper mapperProfile)
        {
            _gsUnitOfWork = unitOfWork;
            _mapperProfile = mapperProfile;
        }

        public async Task<CommentModel> AddCommentAsync(CommentModel commentDTO)
        {
            var commentDAO = _mapperProfile.Map<Comment>(commentDTO);

            var instance = await _gsUnitOfWork.CommentRepository.AddAsync(commentDAO);
            await _gsUnitOfWork.SaveAsync();

            return _mapperProfile.Map<CommentModel>(instance);
        }

        public async Task<CartModel> AddGameToCartAsync(int gameId, int cartId)
        {
            var cartItem = new CartItem
            {
                CartId = cartId,
                GameId = gameId
            };

            await _gsUnitOfWork.CartItemRepository.AddAsync(cartItem);
            await _gsUnitOfWork.SaveAsync();

            var cart = await _gsUnitOfWork.CartRepository.GetByIdAsync(cartId);

            return _mapperProfile.Map<CartModel>(cart);
        }

        public async Task<GameModel> AddNewGameAsync(GameModel gameDTO)
        {
            var game = _mapperProfile.Map<Game>(gameDTO);
            game = await _gsUnitOfWork.GameRepository.AddAsync(game);
            await _gsUnitOfWork.SaveAsync();

            return _mapperProfile.Map<GameModel>(game);
        }

        public async Task ConfirmOrderAsync(OrderModel orderDTO)
        {
            var order = _mapperProfile.Map<Order>(orderDTO);
            await _gsUnitOfWork.OrderRepository.AddAsync(order);
            var cart = await _gsUnitOfWork.CartRepository.GetByIdAsync(order.CartId);

            foreach (var item in cart.Items)
            {
                _gsUnitOfWork.CartItemRepository.Delete(item);
            }

            await _gsUnitOfWork.SaveAsync();
        }

        public async Task<CartItemUpdateResponse> DecreaseGameQuantityAsync(int cartItemId)
        {
            var cartItem = await _gsUnitOfWork.CartItemRepository.GetByIdAsync(cartItemId);
            cartItem.Quantity--;
            _gsUnitOfWork.CartItemRepository.Update(cartItem);
            await _gsUnitOfWork.SaveAsync();

            return new CartItemUpdateResponse
            {
                Quantity = cartItem.Quantity,
                ItemSum = cartItem.Quantity * cartItem.Game.Price,
                CartSum = cartItem.Cart.Items.Sum(ci => ci.Quantity * ci.Game.Price)
            };
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _gsUnitOfWork.CommentRepository.DeleteByIdAsync(id);
            await _gsUnitOfWork.SaveAsync();
        }

        public async Task DeleteGameAsync(int id)
        {
            await _gsUnitOfWork.GameRepository.DeleteByIdAsync(id);
            await _gsUnitOfWork.SaveAsync();
        }

        public async Task<CommentModel> EditCommentAsync(CommentModel commentDTO)
        {
            var comment = _mapperProfile.Map<Comment>(commentDTO);
            _gsUnitOfWork.CommentRepository.Update(comment);
            await _gsUnitOfWork.SaveAsync();

            comment = await _gsUnitOfWork.CommentRepository.GetByIdAsync(comment.Id);
            return _mapperProfile.Map<CommentModel>(comment);
        }

        public async Task<IEnumerable<GenreNodeModel>> GetAllGenreNodesAsync()
        {
            var nodes = await _gsUnitOfWork.GenreNodeRepository.GetAllAsync();

            return _mapperProfile.Map<IEnumerable<GenreNodeModel>>(nodes);
        }

        public async Task<GameModel> GetGameByIdAsync(int id)
        {
            var game = await _gsUnitOfWork.GameRepository.GetByIdAsync(id);

            return _mapperProfile.Map<GameModel>(game);
        }

        public async Task<IEnumerable<GameModel>> GetGamesAsync(FilterOptionsModel options)
        {
            var games = await _gsUnitOfWork.GameRepository.GetAllAsync();

            return _mapperProfile.Map<IEnumerable<GameModel>>(games);
        }

        public async Task<CartItemUpdateResponse> IncreaseGameQuantityAsync(int cartItemId)
        {
            var cartItem = await _gsUnitOfWork.CartItemRepository.GetByIdAsync(cartItemId);
            cartItem.Quantity++;
            _gsUnitOfWork.CartItemRepository.Update(cartItem);
            await _gsUnitOfWork.SaveAsync();

            return new CartItemUpdateResponse
            {
                Quantity = cartItem.Quantity,
                ItemSum = cartItem.Quantity * cartItem.Game.Price,
                CartSum = cartItem.Cart.Items.Sum(ci => ci.Quantity * ci.Game.Price)
            };
        }

        public async Task<CartModel> RemoveGameFromCartAsync(int cartId, int itemId)
        {
            await _gsUnitOfWork.CartItemRepository.DeleteByIdAsync(itemId);
            await _gsUnitOfWork.SaveAsync();

            var cart = await _gsUnitOfWork.CartRepository.GetByIdAsync(cartId);
            return _mapperProfile.Map<CartModel>(cart);
        }

        public async Task<CommentModel> RestoreCommentAsync(int id)
        {
            var comment = await _gsUnitOfWork.CommentRepository.GetByIdAsync(id);
            comment.IsDeleted = false;

            _gsUnitOfWork.CommentRepository.Update(comment);
            await _gsUnitOfWork.SaveAsync();

            return _mapperProfile.Map<CommentModel>(comment);
        }

        public async Task<GameModel> UpdateGame(GameModel gameDTO)
        {
            var game = _mapperProfile.Map<Game>(gameDTO);

            _gsUnitOfWork.GameRepository.Update(game);
            await _gsUnitOfWork.SaveAsync();

            game = await _gsUnitOfWork.GameRepository.GetByIdAsync(game.Id);
            return _mapperProfile.Map<GameModel>(game);
        }
    }
}
