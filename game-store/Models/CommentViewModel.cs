using game_store_business.Models;
using game_store_domain.Entities;

namespace game_store.Models
{
    public class CommentViewModel
    {
        public CommentViewModel(CommentModel model)
            => CommentModel = model;

        public CommentModel CommentModel { get; set; }
        public int CommentLevel { get; set; }
    }
}
