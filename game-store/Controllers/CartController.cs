using game_store_domain.Entities;
using game_store_domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace game_store.Controllers
{
    public class CartController : Controller
    {
        private readonly IGameStoreServices _storeServicesProvider;
        private readonly UserManager<GameStoreUser> _userManager;

        public CartController(IGameStoreServices gameStoreServices, UserManager<GameStoreUser> userManager)
        {
            _storeServicesProvider = gameStoreServices;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return View(currentUser.Cart);
        }

        [HttpPost]
        public void AddGameIntoCart(int gameId, string userId)
        {
            var cart = _storeServicesProvider.AddGameToCart(gameId, userId);

        }

        [HttpPost]
        public JsonResult IncreaseGameQuantity(int cartId, int itemId)
        {
            var result = _storeServicesProvider.IncreaseGameQuantity(cartId, itemId);

            return Json(new { result.quantity, result.itemSum, result.totalSum });
        }

        [HttpPost]
        public JsonResult DecreaseGameQuantity(int cartId, int itemId)
        {
            var result = _storeServicesProvider.DecreaseGameQuantity(cartId, itemId);

            return Json(new { result.quantity, result.itemSum, result.totalSum });
        }

        [HttpPost]
        public decimal RemoveGameFromCart(int cartId, int itemId)
        {
            var cart = _storeServicesProvider.RemoveGameFromCart(cartId, itemId);

            return cart.TotalSum;
        }

        [HttpPost]
        public async Task<IActionResult> MoveToOrder(string userId, int cartId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            return View(new Order
            {
                UserId = userId,
                CartId = cartId,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber
            });
        }

        [HttpPost]
        public IActionResult ConfirmOrder(Order order)
        {
            _storeServicesProvider.ConfirmOrder(order);

            return View("OrderSucceed");
        }
    }
}
