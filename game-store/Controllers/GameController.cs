using game_store.Models;
using game_store_domain.Entities;
using game_store_domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

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
            return View(new GameViewModel(game));
        }

        public IActionResult NewGamePage()
        {
            ViewBag.GenreNodes = _gameServicesProvider.GetAllGenreNodes();
            return View(new EditGameViewModel(new Game()));
        }

        public ActionResult AddGame(EditGameViewModel gameViewModel)
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
            ViewBag.GenreNodes = _gameServicesProvider.GetAllGenreNodes();
            return View(new EditGameViewModel(_gameServicesProvider.GetGameById(gameId)));
        }

        public string UpdateGameData(EditGameViewModel gameViewModel)
        {
            _gameServicesProvider.UpdateGame(gameViewModel);

            var redirectUrl = Url.ActionLink("ViewGame", "Game", new { gameId = gameViewModel.Id });

            return redirectUrl;
        }

        public ActionResult DeleteGame(int gameId)
        {
            _gameServicesProvider.DeleteGame(gameId);

            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpPost]
        public int AddComment([FromForm] string userId, int gameId, int parrentId, string comment)
        {
            var instance = _gameServicesProvider.AddComment(
                new Comment
                {
                    UserId = userId,
                    ParentId = parrentId != 0 ? parrentId : null,
                    GameId = gameId,
                    Text = comment
                });

            return instance.Id;
        }

        [HttpPost]
        public void EditComment([FromForm]int id, string comment)
        {
            _gameServicesProvider.EditComment(
                new Comment
                {
                    Id = id,
                    Text = comment
                });
        }

        [HttpPost]
        public void DeleteComment(int commentId)
        {
            _gameServicesProvider.DeleteComment(commentId);
        }

        [HttpPost]
        public void RestoreComment(int commentId)
        {
            _gameServicesProvider.RestoreComment(commentId);
        }
    }
}
