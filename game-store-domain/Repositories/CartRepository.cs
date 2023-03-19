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
    public class CartRepository : RepositoryBase<Cart>
    {
        public CartRepository(GameStoreDbContext dbContext) : base(dbContext) { }
    }
}
