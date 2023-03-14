using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Entities
{
    public enum PaymentType
    {
        Cash = 1,
        Card = 2
    }

    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
        public PaymentType PaymentType { get; set; }
        public List<OrderItem> Items { get; set; }
        virtual public GameStoreUser User { get; set; }
    }
}
