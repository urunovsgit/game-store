using game_store.Models;
using game_store_business.Models;
using game_store_business.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace game_store.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameServicesProvider;


        public GameController(IGameService gameServices)
        {
            _gameServicesProvider = gameServices;
        }

        [HttpGet("games/{gameId}")]
        public async Task<IActionResult> ViewGame(int gameId)
        {
            var game = await _gameServicesProvider.GetByIdAsync(gameId);
            return View(new SingleGameViewModel(game));
        }

        [HttpGet("games/new")]
        public async Task<IActionResult> NewGame()
        {
            var genreNodes = await _gameServicesProvider.GetAllGenreNodesModelsAsync();
            return View(new EditGameViewModel(new GameModel(), genreNodes));
        }

        [HttpPost]
        public string AddGame(EditGameViewModel gameModel)
        {
            _gameServicesProvider.CreateAsync(gameModel).Wait();
            var redirectUrl = Url.ActionLink("Index", "Home");

            return redirectUrl;
        }

        [HttpGet("games/edit/{gameId}")]
        public async Task<IActionResult> EditGame(int gameId)
        {
            var gameModel = await _gameServicesProvider.GetByIdAsync(gameId);
            var genreNodes = await _gameServicesProvider.GetAllGenreNodesModelsAsync();

            return View(new EditGameViewModel(gameModel, genreNodes));
        }

        [HttpPost]
        public string UpdateGameData(EditGameViewModel gameViewModel)
        {
            _gameServicesProvider.UpdateAsync(gameViewModel).Wait();
            var redirectUrl = Url.ActionLink("ViewGame", "Game", new { gameId = gameViewModel.Id });
            return redirectUrl;
        }

        public async Task<ActionResult> DeleteGame(int gameId)
        {
            await _gameServicesProvider.DeleteByIdAsync(gameId);
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
