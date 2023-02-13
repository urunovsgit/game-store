using game_store_domain.Entities;
using game_store_domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace game_store.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameServices _gameServicesProvider;

        public GameController(ILogger<HomeController> logger, IGameServices gameServices)
        {
            _logger = logger;
            _gameServicesProvider = gameServices;
        }

        public IActionResult ViewGame(int gameId)
        {
            var game = _gameServicesProvider.GetGameById(gameId);
            return View(game);
        }

        public IActionResult NewGamePage()
        {
            return View();
        }

        public ActionResult AddGame(string gameTitle, string gameDescr, string gameOwner, List<Genre> genres)
        {
            var game = new Game
            {
                Title = gameTitle,
                Owner = gameOwner,
                Genres = new List<Genre> { new Genre { Name = "Other"} },
                Description = gameDescr
            };

            //_gameServicesProvider.AddNewGame(game);

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
