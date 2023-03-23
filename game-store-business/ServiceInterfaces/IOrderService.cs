using game_store_business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_business.ServiceInterfaces
{
    public interface IOrderService : ICrudService<OrderModel>
    {
        Task<CartItemUpdateResponse> IncreaseGameQuantityAsync(int cartItemId);
        Task<CartItemUpdateResponse> DecreaseGameQuantityAsync(int cartItemId);
        Task<CartModel> CreateCartForUser(int userId);
        Task<CartModel> GetCartByUserId(int userId);
        Task<CartModel> AddGameToCartAsync(int gameId, int cartId);
        Task<CartModel> RemoveGameFromCartAsync(int cartId, int itemId);
    }
}
