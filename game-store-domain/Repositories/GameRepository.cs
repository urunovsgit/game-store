using game_store_domain.Data;
using game_store_domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace game_store_domain.Repositories
{
    public class GameRepository : RepositoryBase<Game>
    {
        public GameRepository(GameStoreDbContext dbContext) : base(dbContext) { }

        public override void Update(Game entity)
        {
            CheckInstanceWithKeyForNull(entity, entity);

            var game = _storeDbContext.Set<Game>().Find(entity.Id);

            CheckInstanceWithKeyForNull(entity, game);

            game.Title = entity.Title;
            game.Price = entity.Price;
            game.Description = entity.Description;
            game.Image = entity.Image;

            _storeDbContext.Entry(game).State = EntityState.Modified;
        }
    }
}
