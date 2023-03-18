using game_store_domain.Entities;
using game_store_domain.Services.Infrastrucure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Services
{
    public interface IGameStoreServices
    {
        public (IEnumerable<Game>, int) GetGames(FilterPageOptions options);
        public IEnumerable<Game> GetAllGames();
        public IEnumerable<Game> GetGamesByGenres(IEnumerable<Genre> genres);
        public IEnumerable<Game> GetGamesByTitle(string title);
        public Game GetGameById(int id);
        public Game AddNewGame(Game game);
        public Game UpdateGame(Game game);
        public void DeleteGame(int id);
        public Comment AddComment(Comment comment);
        public Comment EditComment(Comment comment);
        public void DeleteComment(int id);
        public Comment RestoreComment(int id);
        public Cart AddGameToCart(int gameId, string userId);
        public Cart RemoveGameFromCart(int cartId, int itemId);
        public (int quantity, decimal itemSum, decimal totalSum) IncreaseGameQuantity(int cartId, int cartItemId);
        public (int quantity, decimal itemSum, decimal totalSum) DecreaseGameQuantity(int cartId, int cartItemId);
        public void ConfirmOrder(Order order);

        public List<GenreNode> GetAllGenreNodes();
    }
}
