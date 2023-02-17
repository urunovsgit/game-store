using game_store.Models;
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
            return View(
                new SingleGameViewModel
                {
                    Game = new Game(),
                    GenreNodes = _gameServicesProvider.GetAllGenreNodes()
                }) ;
        }

        public ActionResult AddGame(string gameTitle, string gameDescr, List<GenreNode> genres)
        {
            var imageFile = Request.Form.Files.First();
            var memStream = new MemoryStream();
            imageFile.CopyTo(memStream);

            var game = new Game
            {
                Title = gameTitle,
                Description = gameDescr,
                Image = memStream.ToArray()
            };
            _gameServicesProvider.AddNewGame(game);

            memStream.Close();
            memStream.Dispose();

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
