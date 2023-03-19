using game_store_domain.Data;
using game_store_domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>
    {
        public CommentRepository(GameStoreDbContext dbContext) : base(dbContext) { }

        public override void Update(Comment entity)
        {
            CheckInstanceWithKeyForNull(entity, entity);

            var comment = _storeDbContext.Set<Comment>().Find(entity.Id);

            CheckInstanceWithKeyForNull(entity, comment);

            comment.Text = entity.Text;
            comment.IsDeleted = entity.IsDeleted;

            _storeDbContext.Entry(comment).State = EntityState.Modified;
        }
    }
}
