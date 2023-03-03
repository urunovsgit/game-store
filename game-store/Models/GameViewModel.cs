using game_store_domain.Entities;

namespace game_store.Models
{
    public class GameViewModel : Game
    {
        public GameViewModel() { }

        public GameViewModel(Game game)
        {
            this.CopyFrom(game);
            Id = game.Id;
        }

        public List<CommentViewModel> LeveledComments { 
            get
            {
                var leveledComments = new List<CommentViewModel>();
                var comments = Comments?.Where(c => c.ParentId == null);
                var currentLevel = 0;

                if(comments == null) return leveledComments;

                foreach (var comment in comments)
                {
                    LevelComments(comment, currentLevel, leveledComments);
                }

                return leveledComments;
            }
        }

        private void LevelComments(Comment current, int currentLevel, List<CommentViewModel> comments)
        {
            var commentVm = new CommentViewModel(current);
            commentVm.CommentLevel = currentLevel;
            comments.Add(commentVm);
            var subcomments = current.SubComments.AsEnumerable();

            if(subcomments != null)
            {
                currentLevel++;

                foreach(Comment comment in subcomments) 
                {
                    LevelComments(comment, currentLevel, comments);
                }
            }
        }
    }
}
