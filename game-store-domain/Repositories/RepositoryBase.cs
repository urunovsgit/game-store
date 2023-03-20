using game_store_domain.Entities;
using game_store_domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace game_store_domain.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _storeDbContext;

        public RepositoryBase(DbContext storeDbContext)
            => _storeDbContext = storeDbContext;

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            CheckInstanceWithKeyForNull(entity, entity);
            var instance = await _storeDbContext.Set<TEntity>().AddAsync(entity);

            return instance.Entity;
        }

        public void Delete(TEntity entity)
        {
            CheckInstanceWithKeyForNull(entity, entity);
            _storeDbContext.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _storeDbContext.Set<TEntity>().FindAsync(id);

            CheckInstanceWithKeyForNull(id, entity);

            _storeDbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _storeDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _storeDbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual void Update(TEntity entity) { return; }

        protected void CheckInstanceWithKeyForNull<TKey>(TKey passedKey, [AllowNull] TEntity entity)
        {
            if (entity == null)
            {
                var message = $"No such {typeof(TEntity)} instance with key: {passedKey}";
                throw new ArgumentException(message, nameof(passedKey));
            }
        }
    }
}
