namespace game_store_business.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
