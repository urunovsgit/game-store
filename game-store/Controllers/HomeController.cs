﻿using game_store.Models;
using game_store_business.Models;
using game_store_business.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace game_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameService _gameServicesProvider;

        public HomeController(IGameService gameServices)
        {
            _gameServicesProvider = gameServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genreNodes = await _gameServicesProvider.GetAllGenreNodesModelsAsync();
            var options = new GamesFilterOptions();
            IEnumerable<GameModel> games;

            if (TempData.ContainsKey("FilterOptions"))
            {
                options = JsonConvert.DeserializeObject<GamesFilterOptions>(TempData["FilterOptions"].ToString());
                games = await _gameServicesProvider.GetGamesByFilter(options);
            }
            else
            {
                games = await _gameServicesProvider.GetAllAsync();
            }
            
            return View(new GamesListViewModel(games, genreNodes, options));
        }

        [HttpPost]
        public void ApplyFilterOptions(GamesFilterOptions options)
        {
            TempData["FilterOptions"] = JsonConvert.SerializeObject(options);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}