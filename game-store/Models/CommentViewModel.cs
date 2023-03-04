using game_store_domain.Entities;

namespace game_store.Models
{
    public class CommentViewModel : Comment
    {
        public CommentViewModel() { }

        public CommentViewModel(Comment comment)
        {
            Id = comment.Id;
            Text = comment.Text;
            DateTime = comment.DateTime;
            User = comment.User;
            UserId = comment.UserId;
            ParentId = comment.ParentId;
            RelatedTo = comment.RelatedTo;
            SubComments = comment.SubComments;
            GameId = comment.GameId;
            Game = comment.Game;
        }
        public int CommentLevel { get; set; }
    }
}
