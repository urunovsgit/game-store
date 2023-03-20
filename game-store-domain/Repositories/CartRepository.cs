using game_store_domain.Data;
using game_store_domain.Entities;

namespace game_store_domain.Repositories
{
    public class CartRepository : RepositoryBase<Cart>
    {
        public CartRepository(GameStoreDbContext dbContext) : base(dbContext) { }
    }
}
