namespace game_store_business.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int? ParentId { get; set; }
        public ICollection<int> SubCommentsIds { get; set; }
    }
}
