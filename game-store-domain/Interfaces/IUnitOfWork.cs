using game_store_domain.Entities;
using game_store_domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Data.Interfaces
{
    public interface IUnitOfWork
    {
        RepositoryBase<Game> GameRepository { get; }
        RepositoryBase<Cart> CartRepository { get; }
        RepositoryBase<CartItem> CartItemRepository { get; }
        RepositoryBase<Order> OrderRepository { get; }
        RepositoryBase<Comment> CommentRepository { get; }
        RepositoryBase<GenreNode> GenreNodeRepository { get; }
        UserManager<GameStoreUser> UserManager { get; }

        Task SaveAsync();
    }
}
