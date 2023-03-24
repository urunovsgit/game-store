using game_store.Models;
using game_store_business.Models;
using game_store_business.ServiceInterfaces;
using game_store_domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace game_store.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartServiceProvider;

        public CartController(ICartService cartService)
        {
            _cartServiceProvider = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int userId)
        {
            var cartModel = await _cartServiceProvider.GetCartByUserId(userId);
            return View(new CartViewModel(cartModel));
        }

        [HttpPost]
        public async Task AddGameToCart(int gameId, int cartId)
        {
            await _cartServiceProvider.AddGameToCartAsync(gameId, cartId);
        }

        [HttpPost]
        public async Task<string> IncreaseGameQuantity(int itemId)
        {
            var result = await _cartServiceProvider.IncreaseGameQuantityAsync(itemId);
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public async Task<string> DecreaseGameQuantity(int itemId)
        {
            var result = await _cartServiceProvider.DecreaseGameQuantityAsync(itemId);
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public async Task<decimal> RemoveGameFromCart(int cartId, int itemId)
        {
            var cart = await _cartServiceProvider.RemoveGameFromCartAsync(cartId, itemId);
            return cart.Sum;
        }

        [HttpGet]
        public async Task<IActionResult> Order(int cartId)
        {
            var order = await _cartServiceProvider.CreateOrderForCartAsync(cartId);
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(OrderModel order)
        {
            await _cartServiceProvider.ConfirmOrderCreationAsync(order);
            return View("OrderSucceed");
        }
    }
}
