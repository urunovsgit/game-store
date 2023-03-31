using game_store_domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace game_store_domain.Repositories
{
    public class GenreNodeRepository : RepositoryBase<GenreNode>
    {
        public GenreNodeRepository(DbContext storeDbContext) : base(storeDbContext) { }

        public override void Update(GenreNode entity)
        {
            CheckInstanceWithKeyForNull(entity, entity);

            var genreNode = _storeDbContext.Set<GenreNode>().Find(entity.Id);

            genreNode.Genre = entity.Genre;

            _storeDbContext.Entry(genreNode).State = EntityState.Modified;
        }
    }
}
