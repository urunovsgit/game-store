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

        public async Task<IActionResult> ViewGame(int gameId)
        {
            var game = await _storeServicesProvider.GetGameByIdAsync(gameId);
            return View(new SingleGameViewModel(game));
        }

        public async Task<IActionResult> NewGame()
        {
            return View(new EditGameViewModel(new GameModel(),
                await _storeServicesProvider.GetAllGenreNodesModelsAsync()));
        }

        [HttpPost]
        public string AddGame(EditGameViewModel gameModel)
        {
            _storeServicesProvider.AddNewGameAsync(gameModel).Wait();
            var redirectUrl = Url.ActionLink("Index", "Home");

            return redirectUrl;
        }

        //public ActionResult EditGameData(int gameId)
        //{
        //    return RedirectToAction(nameof(EditGamePage), new { gameId });
        //}

        public async Task<IActionResult> EditGame(int gameId)
        {
            var gameModel = await _storeServicesProvider.GetGameByIdAsync(gameId);
            var genreNodes = await _storeServicesProvider.GetAllGenreNodesModelsAsync();

            return View(new EditGameViewModel(gameModel, genreNodes));
        }

        public string UpdateGameData(EditGameViewModel gameViewModel)
        {
            _storeServicesProvider.UpdateGame(gameViewModel).Wait();

            var redirectUrl = Url.ActionLink("ViewGame", "Game", new { gameId = gameViewModel.Id });
            return redirectUrl;
        }

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
