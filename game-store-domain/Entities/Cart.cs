using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Entities
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public int? OrderId { get; set; }
        virtual public GameStoreUser User { get; set; }
        virtual public Order Order { get; set; }
        virtual public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
