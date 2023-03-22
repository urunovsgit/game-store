using game_store.Models;
using game_store_business.Models;
using game_store_business.ServiceInterfaces;
using game_store_domain.Entities;
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
            var genreNodes = await _storeServicesProvider.GetAllGenreNodesModelsAsync();
            var options = new GamesFilterOptions();

            if (TempData.ContainsKey("FilterOptions"))
            {
                options = JsonConvert.DeserializeObject<GamesFilterOptions>(Convert.ToString(TempData["FilterOptions"]));
            }
            else
            {
                options.AppliedGenres = genreNodes.Select(gn => (int)gn.Genre).ToList();
            }

            var games = await _storeServicesProvider.GetGamesAsync(options);

            return View(new GamesListViewModel(games, genreNodes, options));
        }

        [HttpPost]
        public void ApplyFilterOptions(GamesFilterOptions options)
        {
            TempData["FilterOptions"] = JsonConvert.SerializeObject(options);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}