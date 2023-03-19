using game_store_domain.Entities;
using game_store_domain.Repositories;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUnitOfWork
    {
        RepositoryBase<Game> GameRepository { get; }
        RepositoryBase<Cart> CartRepository { get; }
        RepositoryBase<CartItem> CartItemRepository { get; }
        RepositoryBase<Order> OrderRepository { get; }
        RepositoryBase<Comment> CommentRepository { get; }

        Task SaveAsync();
    }
}
