using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Entities
{
    public class OrderItem : GameItem
    {
        public int OrderId { get; set; }
        virtual public Order Order { get; set; }

        public OrderItem() { }

        public OrderItem(Game game) : base(game) { }
    }
}
