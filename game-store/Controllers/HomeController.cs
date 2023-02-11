using game_store.Models;
using game_store_domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace game_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameServices _gameServicesProvider;

        public HomeController(ILogger<HomeController> logger, IGameServices gameServices)
        {
            _logger = logger;
            _gameServicesProvider = gameServices;
        }

        public IActionResult Index(int page = 1)
        {
            var options = new SortFilterPageOptions { Page = page };
            var (games, count) = _gameServicesProvider.GetGames(options);

            return View(new GamesViewModel
            {
                Games = games,
                Options = options,
                TotalGamesCount = count
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}