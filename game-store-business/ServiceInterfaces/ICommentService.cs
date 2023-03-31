using game_store_business.Models;

namespace game_store_business.ServiceInterfaces
{
    public interface ICommentService : ICrudService<CommentModel>
    {
        Task<CommentModel> RestoreCommentAsync(int id);
    }
}
