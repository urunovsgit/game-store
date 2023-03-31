namespace game_store_business.Models
{
    public class CartItemUpdateResponse
    {
        public int Quantity { get; set; }
        public decimal ItemSum { get; set; }
        public decimal CartSum { get; set; }
    }
}
