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
                (
                    new Game(),
                    _gameServicesProvider.GetAllGenreNodes()
                ));
        }

        public ActionResult AddGame(SingleGameViewModel gameViewModel)
        {
            _gameServicesProvider.AddNewGame(gameViewModel);

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
                (
                    _gameServicesProvider.GetGameById(gameId),
                    _gameServicesProvider.GetAllGenreNodes()
                ));
        }

        public ActionResult UpdateGameData(SingleGameViewModel gameViewModel)
        {
            _gameServicesProvider.UpdateGame(gameViewModel);

            return RedirectToAction(nameof(ViewGame), new { gameId = gameViewModel.Id });
        }

        public ActionResult DeleteGame(int gameId)
        {
            _gameServicesProvider.DeleteGame(gameId);

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
