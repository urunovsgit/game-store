using game_store.Models;
using game_store_domain.Entities;
using game_store_domain.Services;
using game_store_domain.Services.Infrastrucure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace game_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameStoreServices _storeServicesProvider;

        public HomeController(ILogger<HomeController> logger, IGameStoreServices gameServices)
        {
            _logger = logger;
            _storeServicesProvider = gameServices;
        }


        public IActionResult Index(string titleKey = "")
        {
            
            ViewBag.GenreNodes = _storeServicesProvider.GetAllGenreNodes();
            List<Game> games;

            if(!string.IsNullOrEmpty(titleKey)) 
            {
                games = _storeServicesProvider.GetGamesByTitle(titleKey).ToList();
                ViewBag.SelectedGenres = Enum.GetValues(typeof(Genre)).OfType<Genre>().ToList();
            }
            else if (TempData.ContainsKey("SelectedGenres"))
            {
                var genres = JsonConvert.DeserializeObject<List<Genre>>((string)TempData["SelectedGenres"]);
                games = _storeServicesProvider.GetGamesByGenres(genres).ToList();
                ViewBag.SelectedGenres = genres;
            }
            else
            {
                games = _storeServicesProvider.GetAllGames().ToList();
                ViewBag.SelectedGenres = Enum.GetValues(typeof(Genre)).OfType<Genre>().ToList();
            }

            return View(new GamesViewModel { Games = games, TitleKey = titleKey });
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

        [Authorize]
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