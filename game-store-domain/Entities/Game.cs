using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Entities
{
    public class Game: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        virtual public List<Genre> Genres { get; set; }
        virtual public List<Comment> Comments { get; set; }
        virtual public List<CartItem> CartItems { get; set; }
    }
}
