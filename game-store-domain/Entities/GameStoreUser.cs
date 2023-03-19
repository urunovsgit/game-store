using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace game_store_domain.Entities
{
    public class GameStoreUser : IdentityUser
    {
        [Required, StringLength(20)]
        public string FirstName { get; set; }

        [Required, StringLength(20)]
        public string LastName { get; set; }

        public byte[] AvatarImage { get; set; }

        virtual public List<Comment> Comments { get; set; }

        virtual public Cart Cart { get; set; }
        virtual public List<Order> Orders { get; set; }
    }
}
