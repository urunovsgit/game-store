using game_store_business.Models;

namespace game_store.Models
{
    public class CartViewModel
    {
        public CartViewModel(CartModel cart)
        {
            CartModel = cart;
            Items = new List<CartItemViewModel>();            
            cart.Items.ForEach(i => Items.Add(new CartItemViewModel(i)));
        }

        public CartModel CartModel { get; set; }
        public decimal TotalSum { get => Items.Sum(i => i.Sum); }
        public List<CartItemViewModel> Items { get; set; }
    }
}
