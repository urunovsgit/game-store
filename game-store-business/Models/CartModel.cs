using game_store_domain.Entities;
using System.Collections.ObjectModel;

namespace game_store_business.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? OrderId { get; set; }
        virtual public List<CartItemModel> Items { get; set; }
    }
}
