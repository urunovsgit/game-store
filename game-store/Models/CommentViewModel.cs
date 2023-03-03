using game_store_domain.Entities;

namespace game_store.Models
{
    public class CommentViewModel : Comment
    {
        public CommentViewModel() { }

        public CommentViewModel(Comment comment)
        {
            Text = comment.Text;
            this.DateTime = comment.DateTime;
            this.User = comment.User;
            this.UserId = comment.UserId;
            this.ParentId = comment.ParentId;
            this.RelatedTo = comment.RelatedTo;
            this.SubComments = comment.SubComments;
            this.GameId = comment.GameId;
            this.Game = comment.Game;
        }
        public int CommentLevel { get; set; }
    }
}
