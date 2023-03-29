namespace game_store_domain.Entities
{
    public class CartItem : BaseEntity
    {
        public int GameId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        virtual public Cart Cart { get; set; }
        virtual public Game Game { get; set; }
    }
}
