using Data.Interfaces;
using game_store_domain.Entities;
using game_store_domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace game_store_domain.Data
{
    public class GSUnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDbContext _storeDbContext;

        public GSUnitOfWork(GameStoreDbContext storeDbContext, UserManager<GameStoreUser> userManager)
        {
            _storeDbContext = storeDbContext;
            UserManager = userManager;
        }

        public RepositoryBase<Game> GameRepository => new GameRepository(_storeDbContext);

        public RepositoryBase<Cart> CartRepository => new CartRepository(_storeDbContext);

        public RepositoryBase<CartItem> CartItemRepository => new CartItemRepository(_storeDbContext);

        public RepositoryBase<Order> OrderRepository => new OrderRepository(_storeDbContext);

        public RepositoryBase<Comment> CommentRepository => new CommentRepository(_storeDbContext);

        public RepositoryBase<GenreNode> GenreNodeRepository => new GenreNodeRepository(_storeDbContext);

        public UserManager<GameStoreUser> UserManager { get; private set; }

        public async Task SaveAsync()
        {
            await _storeDbContext.SaveChangesAsync();
        }
    }
}
