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
