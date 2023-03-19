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
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(GameStoreDbContext storeDbContext) : base(storeDbContext) { }

        public override void Update(Order entity)
        {
            CheckInstanceWithKeyForNull(entity, entity);

            var order = _storeDbContext.Set<Order>().Find(entity.Id);

            CheckInstanceWithKeyForNull(entity, order);

            order.FirstName = entity.FirstName;
            order.LastName = entity.LastName;
            order.PhoneNumber = entity.PhoneNumber;
            order.Email = entity.Email;
            order.Comment = entity.Comment;
            order.PaymentType = entity.PaymentType;

            _storeDbContext.Entry(order).State = EntityState.Modified;
        }
    }
}
