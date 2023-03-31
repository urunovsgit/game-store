using game_store_business.Models;

namespace game_store.Models
{
    public class SingleGameViewModel : GameViewModelBase
    {
        public SingleGameViewModel(GameModel model) : base(model) { }

        public List<CommentViewModel> LeveledComments
        {
            get
            {
                var leveledComments = new List<CommentViewModel>();
                var comments = CommentModels?.Where(c => c.ParentId == null);
                var currentLevel = 0;

                if (comments == null) return leveledComments;

                foreach (var comment in comments)
                {
                    LevelComments(comment, currentLevel, leveledComments);
                }

                return leveledComments;
            }
        }

        private void LevelComments(CommentModel current, int currentLevel, List<CommentViewModel> comments)
        {
            var commentVm = new CommentViewModel(current);
            commentVm.CommentLevel = currentLevel;
            comments.Add(commentVm);
            var subcomments = CommentModels.Where(cm => current.SubCommentsIds.Any(id => id == cm.Id));

            if (subcomments != null)
            {
                currentLevel++;

                foreach (var comment in subcomments)
                {
                    LevelComments(comment, currentLevel, comments);
                }
            }
        }
    }
}
