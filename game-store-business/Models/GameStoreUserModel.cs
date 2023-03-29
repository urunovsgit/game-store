using Microsoft.AspNetCore.Identity;

namespace game_store_business.Models
{
    public class GameStoreUserModel : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarImage { get; set; }
        public ICollection<int> CommentsIds { get; set; }
        public ICollection<int> OrdersIds { get; set; }
    }
}
