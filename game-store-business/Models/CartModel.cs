using System.Collections.ObjectModel;

namespace game_store_business.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? OrderId { get; set; }
        public ICollection<int> CartItemsIds { get; set; } = new Collection<int>();
    }
}
