using game_store_domain.Data;
using game_store_domain.Entities;
using game_store_domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Repositories
{
    public class CartItemRepository : RepositoryBase<CartItem>
    {
        public CartItemRepository(GameStoreDbContext dbContext) : base(dbContext) { }

        public override void Update(CartItem entity)
        {
            CheckInstanceWithKeyForNull(entity, entity);

            var item = _storeDbContext.Set<CartItem>().Find(entity.Id);

            CheckInstanceWithKeyForNull(entity, item);

            item.Quantity = entity.Quantity;
            _storeDbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
