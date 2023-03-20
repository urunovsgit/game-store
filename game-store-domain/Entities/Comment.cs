namespace game_store_domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Text{ get; set; }
        public DateTime DateTime { get; set; }
        public bool IsDeleted { get; set; }
        virtual public GameStoreUser User { get; set; }
        virtual public int UserId { get; set; }
        virtual public Game Game { get; set; }
        virtual public int GameId { get; set; }
        virtual public int? ParentId { get; set; }
        virtual public Comment RelatedTo { get; set; }
        virtual public List<Comment> SubComments { get; set; }
    }
}
