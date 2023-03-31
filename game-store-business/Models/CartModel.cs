namespace game_store_business.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? OrderId { get; set; }
        public decimal Sum { get => Items.Sum(i => i.Quantity * i.GamePrice); }
        virtual public List<CartItemModel> Items { get; set; }
    }
}
