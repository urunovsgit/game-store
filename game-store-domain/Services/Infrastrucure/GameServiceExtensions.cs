using Azure;
using game_store_domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Services.Infrastrucure
{
    public static class GameServiceExtensions
    {
        private static readonly Dictionary<GameOption,
            Func<IQueryable<Game>, FilterPageOptions, IEnumerable<Game>>> _filterFunctions;

        static GameServiceExtensions()
        {
            _filterFunctions = new Dictionary<GameOption, Func<IQueryable<Game>, FilterPageOptions, IEnumerable<Game>>>
            {
                { GameOption.Genre, FilterByGenre },
                { GameOption.Title, FilterByTitle },
                { GameOption.None, FilterByDefault }
            };
        }

        public static IEnumerable<Game> FilterByGenres(this IQueryable<Game> gamesQuery, IEnumerable<Genre> genres)
        {
            return gamesQuery.ToList()
                             .Where(game => game.Genres
                                 .Any(genre => genres
                                 .Contains(genre)))
                             .ToList();
        }

        public static IEnumerable<Game> Filter(this IQueryable<Game> queryable, FilterPageOptions options)
        {
            var filteredGames = new List<Game>();

            while (filteredGames.Count < options.PageSize)
            {
                foreach (var filterUnit in options.FilterValueUnits)
                {
                    filteredGames.AddRange(_filterFunctions[filterUnit.Key](queryable, options));
                }
            }

            return filteredGames.Take(options.PageSize);
        }

        private static IEnumerable<Game> FilterByGenre(IQueryable<Game> gamesQuery, FilterPageOptions options)
        {
            var filteredGames = new List<Game>();
            var pageOffset = options.Page;
            var genresList = (List<Genre>)options.FilterValueUnits[GameOption.Genre];

            while (filteredGames.Count < options.PageSize)
            {
                var items = gamesQuery.Skip((pageOffset - 1) * options.PageSize)
                                      .Take(options.PageSize)
                                      .ToList()
                                      .Where(game => game.Genres
                                         .Any(genre => genresList
                                            .Contains(genre)))
                                      .ToList();

                if (items.Count == 0) break;

                items.ForEach(item =>
                {
                    if (filteredGames.Count < options.PageSize)
                        filteredGames.Add(item);
                });

                ++pageOffset;
            }

            return filteredGames;
        }

        private static IEnumerable<Game> FilterByTitle(IQueryable<Game> gamesQuery, FilterPageOptions options)
        {
            var filteredGames = new List<Game>();
            var pageOffset = options.Page;
            var substring = (string)options.FilterValueUnits[GameOption.Title];

            if (substring == null || substring.Length < 3)
            {
                return filteredGames;
            }

            while (filteredGames.Count < options.PageSize)
            {
                var items = gamesQuery.Skip((pageOffset - 1) * options.PageSize)
                                      .Take(options.PageSize)
                                      .ToList()
                                      .Where(game => game.Title
                                         .Contains(substring, StringComparison.InvariantCultureIgnoreCase))
                                      .ToList();

                if (items.Count == 0) break;

                items.ForEach(item =>
                {
                    if (filteredGames.Count < options.PageSize)
                        filteredGames.Add(item);
                });

                ++pageOffset;
            }

            return filteredGames;
        }

        private static IEnumerable<Game> FilterByDefault(IQueryable<Game> gamesQuery, FilterPageOptions options)
        {
            return gamesQuery.Skip((options.Page - 1) * options.PageSize)
                             .Take(options.PageSize)
                             .ToList();
        }
    }
}
