using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Entities
{
    public class CartItem : GameItem
    {
        public int CartId { get; set; }
        virtual public Cart Cart { get; set; }

        public CartItem() { }
        public CartItem(Game game) : base(game) { }
    }
}
