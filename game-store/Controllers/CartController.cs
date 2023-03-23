using game_store.Models;
using game_store_business.ServiceInterfaces;
using game_store_domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace game_store.Controllers
{
    public class CartController : Controller
    {
        private readonly IOrderService _orderServiceProvider;

        public CartController(IOrderService orderService)
        {
            _orderServiceProvider = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int userId)
        {
            var cartModel = await _orderServiceProvider.GetCartByUserId(userId);
            return View(new CartViewModel(cartModel));
        }

        [HttpPost]
        public async Task AddGameToCart(int gameId, int cartId)
        {
            await _orderServiceProvider.AddGameToCartAsync(gameId, cartId);
        }

        //[HttpPost]
        //public JsonResult IncreaseGameQuantity(int cartId, int itemId)
        //{
        //    var result = _storeServicesProvider.IncreaseGameQuantity(cartId, itemId);

        //    return Json(new { result.quantity, result.itemSum, result.totalSum });
        //}

        //[HttpPost]
        //public JsonResult DecreaseGameQuantity(int cartId, int itemId)
        //{
        //    var result = _storeServicesProvider.DecreaseGameQuantity(cartId, itemId);

        //    return Json(new { result.quantity, result.itemSum, result.totalSum });
        //}

        //[HttpPost]
        //public decimal RemoveGameFromCart(int cartId, int itemId)
        //{
        //    var cart = _storeServicesProvider.RemoveGameFromCart(cartId, itemId);

        //    return cart.TotalSum;
        //}

        //[HttpPost]
        //public async Task<IActionResult> MoveToOrder(string userId, int cartId)
        //{
        //    var currentUser = await _userManager.GetUserAsync(User);

        //    return View(new Order
        //    {
        //        UserId = userId,
        //        CartId = cartId,
        //        FirstName = currentUser.FirstName,
        //        LastName = currentUser.LastName,
        //        Email = currentUser.Email,
        //        PhoneNumber = currentUser.PhoneNumber
        //    });
        //}

        //[HttpPost]
        //public IActionResult ConfirmOrder(Order order)
        //{
        //    _storeServicesProvider.ConfirmOrder(order);

        //    return View("OrderSucceed");
        //}
    }
}
