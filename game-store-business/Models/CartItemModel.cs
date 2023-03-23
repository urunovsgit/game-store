namespace game_store_business.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public string GameTitle { get; set; }
        public decimal GamePrice { get; set; }
        public byte[] GameImage { get; set; }
    }
}
