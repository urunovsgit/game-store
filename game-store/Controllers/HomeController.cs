using game_store.Models;
using game_store_business.Models;
using game_store_domain.Entities;
using game_store_domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace game_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameStoreServices _storeServicesProvider;

        public HomeController(IGameStoreServices gameServices)
        {
            _storeServicesProvider = gameServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var options = new GamesFilterOptions();

            if (TempData.ContainsKey("FilterOptions"))
            {
                options = JsonConvert.DeserializeObject<GamesFilterOptions>(Convert.ToString(TempData["FilterOptions"]));
            }

            return View(new GamesListViewModel(
                await _storeServicesProvider.GetGamesAsync(options),
                await _storeServicesProvider.GetAllGenreNodesModelsAsync(),
                options));
        }

        [HttpPost]
        public void ApplyFilterOptions(GamesFilterOptions options)
        {
            TempData["FilterOptions"] = JsonConvert.SerializeObject(options);
        }

        //[Authorize]
        //public ActionResult AddGame()
        //{
        //    return RedirectToAction(nameof(AddGame), nameof(GameController));
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}