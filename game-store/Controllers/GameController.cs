using game_store.Models;
using game_store_business.Models;
using game_store_domain.Entities;
using game_store_domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace game_store.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameStoreServices _storeServicesProvider;

        public GameController(IGameStoreServices gameServices)
        {
            _storeServicesProvider = gameServices;
        }

        public IActionResult ViewGame(int gameId)
        {
            var game = _storeServicesProvider.GetGameByIdAsync(gameId).Result;

            return View(new GameViewModel
            {
                GameModel = game
            });
        }

        //public IActionResult NewGamePage()
        //{
        //    ViewBag.GenreNodes = _storeServicesProvider.GetAllGenreNodes();
        //    return View(new EditGameViewModel(new Game()));
        //}

        //public ActionResult AddGame(EditGameViewModel gameViewModel)
        //{
        //    _storeServicesProvider.AddNewGame(gameViewModel);

        //    return RedirectToAction(nameof(Index), "Home");
        //}

        //public ActionResult EditGameData(int gameId)
        //{
        //    return RedirectToAction(nameof(EditGamePage), new { gameId });
        //}

        //public IActionResult EditGamePage(int gameId)
        //{
        //    ViewBag.GenreNodes = _storeServicesProvider.GetAllGenreNodes();
        //    return View(new EditGameViewModel(_storeServicesProvider.GetGameById(gameId)));
        //}

        //public string UpdateGameData(EditGameViewModel gameViewModel)
        //{
        //    _storeServicesProvider.UpdateGame(gameViewModel);

        //    var redirectUrl = Url.ActionLink("ViewGame", "Game", new { gameId = gameViewModel.Id });

        //    return redirectUrl;
        //}

        //public ActionResult DeleteGame(int gameId)
        //{
        //    _storeServicesProvider.DeleteGame(gameId);

        //    return RedirectToAction(nameof(Index), "Home");
        //}

        //[HttpPost]
        //public int AddComment([FromForm] string userId, int gameId, int parrentId, string comment)
        //{
        //    var instance = _storeServicesProvider.AddComment(
        //        new Comment
        //        {
        //            UserId = userId,
        //            ParentId = parrentId != 0 ? parrentId : null,
        //            GameId = gameId,
        //            Text = comment
        //        });

        //    return instance.Id;
        //}

        //[HttpPost]
        //public void EditComment([FromForm]int id, string comment)
        //{
        //    _storeServicesProvider.EditComment(
        //        new Comment
        //        {
        //            Id = id,
        //            Text = comment
        //        });
        //}

        //[HttpPost]
        //public void DeleteComment(int commentId)
        //{
        //    _storeServicesProvider.DeleteComment(commentId);
        //}

        //[HttpPost]
        //public void RestoreComment(int commentId)
        //{
        //    _storeServicesProvider.RestoreComment(commentId);
        //}
    }
}
