using game_store_business.Models;

namespace game_store.Models
{
    public class CommentViewModel : CommentModel
    {
        public CommentViewModel() { }
        public CommentViewModel(CommentModel model)
        {
            Id = model.Id;
            Text = model.Text;
            Username = model.Username;
            DateTime = model.DateTime;
            IsDeleted = model.IsDeleted;
            UserId = model.UserId;
            GameId = model.GameId;
            ParentId = model.ParentId;
            SubCommentsIds = model.SubCommentsIds;
        }
        public int CommentLevel { get; set; }
    }
}
