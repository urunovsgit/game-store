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
                });
        }

        public ActionResult AddGame(string gameTitle, string gameDescr, List<GenreNode> genres)
        {
            var imageFile = Request.Form.Files.FirstOrDefault();
            var memStream = new MemoryStream();
            imageFile?.CopyTo(memStream);

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

        public ActionResult EditGameData(int gameId)
        {
            return RedirectToAction(nameof(EditGamePage), new { gameId });
        }

        public IActionResult EditGamePage(int gameId)
        {
            return View(
                new SingleGameViewModel
                {
                    Game = _gameServicesProvider.GetGameById(gameId),
                    GenreNodes = _gameServicesProvider.GetAllGenreNodes()
                });
        }

        public ActionResult UpdateGameData(int gameId, string gameTitle, decimal gamePrice, string gameDescr, List<Genre> genres)
        {
            var game = new Game
            {
                Id = gameId,
                Title = gameTitle,
                Genres = genres,
                Price = gamePrice,
                Description = gameDescr
            };

            var imageFile = Request.Form.Files.FirstOrDefault();
            var memStream = new MemoryStream();

            if (imageFile != null)
            {
                imageFile.CopyTo(memStream);
                game.Image = memStream.ToArray();
            }

            _gameServicesProvider.UpdateGame(game);

            memStream.Close();
            memStream.Dispose();

            return RedirectToAction(nameof(ViewGame), new { gameId });
        }

        public ActionResult DeleteGame(int gameId)
        {
            _gameServicesProvider.DeleteGame(gameId);

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
