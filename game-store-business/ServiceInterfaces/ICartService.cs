using game_store_business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_business.ServiceInterfaces
{
    public interface ICartService : ICrudService<CartModel>
    {
        Task<CartItemUpdateResponse> IncreaseGameQuantityAsync(int cartItemId);
        Task<CartItemUpdateResponse> DecreaseGameQuantityAsync(int cartItemId);
        Task<OrderModel> CreateOrderForCartAsync(int cartId);
        Task<OrderModel> ConfirmOrderCreationAsync(OrderModel orderModel);
        Task<CartModel> GetCartByUserId(int userId);
        Task<CartModel> AddGameToCartAsync(int gameId, int cartId);
        Task<CartModel> RemoveGameFromCartAsync(int cartId, int itemId);
    }
}
