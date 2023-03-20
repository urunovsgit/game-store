using game_store.Infrastructure;
using game_store_business.Models;
using System.ComponentModel.DataAnnotations;

namespace game_store.Models
{
    public class GameViewModel
    {
        public GameModel GameModel { get; set; }
        public IEnumerable<string> GenreNames { 
            get 
            {
                var genreNames = new List<string>();

                foreach (var genre in GameModel.Genres)
                {
                    genreNames.Add(genre.GetAttribute<DisplayAttribute>().Name);
                }

                return genreNames;
            } 
        }

        public List<CommentViewModel> LeveledComments { 
            get
            {
                var leveledComments = new List<CommentViewModel>();
                var comments = GameModel.CommentModels?.Where(c => c.ParentId == null);
                var currentLevel = 0;

                if(comments == null) return leveledComments;

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
            var subcomments = GameModel.CommentModels.Where(cm => current.SubCommentsIds.Any(id => id == cm.Id));

            if(subcomments != null)
            {
                currentLevel++;

                foreach(var comment in subcomments) 
                {
                    LevelComments(comment, currentLevel, comments);
                }
            }
        }
    }
}
