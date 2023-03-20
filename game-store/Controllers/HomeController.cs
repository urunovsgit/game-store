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
            //ViewBag.GenreNodes = await _storeServicesProvider.GetAllGenreNodesModelsAsync();
            var options = new GamesFilterOptions();

            return View(new GamesListViewModel
            {
                Games = await _storeServicesProvider.GetGamesAsync(options),
                FilterOptions = options,
                AllGenreNodes = await _storeServicesProvider.GetAllGenreNodesModelsAsync()
            });

            //if(!string.IsNullOrEmpty(titleKey)) 
            //{
            //    games = _storeServicesProvider.GetGamesByTitle(titleKey).ToList();
            //    ViewBag.SelectedGenres = Enum.GetValues(typeof(Genre)).OfType<Genre>().ToList();
            //}
            //else if (TempData.ContainsKey("SelectedGenres"))
            //{
            //    var genres = JsonConvert.DeserializeObject<List<Genre>>((string)TempData["SelectedGenres"]);
            //    games = _storeServicesProvider.GetGamesByGenres(genres).ToList();
            //    ViewBag.SelectedGenres = genres;
            //}
            //else
            //{
            //    games = _storeServicesProvider.GetAllGames().ToList();
            //    ViewBag.SelectedGenres = Enum.GetValues(typeof(Genre)).OfType<Genre>().ToList();
            //}

            //return View(new GamesViewModel { Games = games, TitleKey = titleKey });
        }

        [HttpPost]
        public async Task<IActionResult> Index(GamesFilterOptions options)
        {
            return View(new GamesListViewModel
            {
                Games = await _storeServicesProvider.GetGamesAsync(options),
                FilterOptions = options,
                AllGenreNodes = await _storeServicesProvider.GetAllGenreNodesModelsAsync()
            });
        }

            [HttpPost]
        public void ApplyFilterOptions(GamesFilterOptions options)
        {
            RedirectToAction(nameof(Index), new { options.AppliedGenresIds, options.TitleSubstring });
        }

        [HttpPost]
        public ActionResult FindByGameTitle(string titleToFind)
        {
            return RedirectToAction("Index", new {titleKey = titleToFind });
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