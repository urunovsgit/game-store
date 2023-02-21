using game_store.Models;
using game_store_domain.Entities;
using game_store_domain.Services;
using game_store_domain.Services.Infrastrucure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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


        public IActionResult Index(string titleKey = "")
        {
            ViewBag.SelectedGenres = Enum.GetValues(typeof(Genre)).OfType<Genre>().ToList();
            ViewBag.GenreNodes = _gameServicesProvider.GetAllGenreNodes();
            List<Game> games;

            if(!string.IsNullOrEmpty(titleKey)) 
            {
                games = _gameServicesProvider.GetGamesByTitle(titleKey).ToList();
            }
            else if (!TempData.ContainsKey("SelectedGenres"))
            {
                games = _gameServicesProvider.GetAllGames().ToList();
            }
            else
            {
                var genres = JsonConvert.DeserializeObject<List<Genre>>((string)TempData["SelectedGenres"]);
                games = _gameServicesProvider.GetGamesByGenres(genres).ToList();
            }

            return View(new GamesViewModel { Games = games });
        }

        [HttpPost]
        public void ApplyGenreFilterOptions(List<Genre> genres)
        {
            TempData["SelectedGenres"] = JsonConvert.SerializeObject(genres);
        }

        [HttpPost]
        public ActionResult FindByGameTitle(string titleToFind)
        {
            return RedirectToAction("Index", new {titleKey = titleToFind });
        }

        public ActionResult AddGame()
        {
            return RedirectToAction(nameof(AddGame), nameof(GameController));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}