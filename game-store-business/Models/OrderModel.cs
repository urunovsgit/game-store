using game_store_domain.Entities;

namespace game_store_business.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Comment { get; set; }
        public decimal TotalSum { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
