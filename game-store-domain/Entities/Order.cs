using System.ComponentModel.DataAnnotations;

namespace game_store_domain.Entities
{
    public enum PaymentType
    {
        [Display(Name = "Cash")]
        Cash = 1,

        [Display(Name = "Card")]
        Card = 2
    }

    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Comment { get; set; }
        public decimal TotalSum { get; set; }
        public PaymentType PaymentType { get; set; }
        virtual public Cart Cart { get; set; }
        virtual public GameStoreUser User { get; set; }
    }
}
