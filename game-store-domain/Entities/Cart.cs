using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        virtual public GameStoreUser User { get; set; }
        virtual public List<CartItem> Items { get; set; }

        [NotMapped]
        public decimal TotalSum
        {
            get { return Items.Sum(i => i.Sum); }
        }
    }
}
