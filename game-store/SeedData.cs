using game_store_domain.Data;
using game_store_domain.Entities;

namespace game_store
{
    public class SeedData
    {
        public void Init(GSUnitOfWork gsUnitOfWork)
        {
            EnsureCreatedGameGenres(gsUnitOfWork);
            EnsurePopulatedWithDemoData(gsUnitOfWork);
        }

        private void EnsureCreatedGameGenres(GSUnitOfWork _gsUnitOfWork)
        {
            var genres = _gsUnitOfWork.GenreNodeRepository.GetAllAsync().Result;

            if (genres.Any())
            {
                return;
            }

            var genresData = new List<GenreNode>
            {
                new GenreNode
                {
                    Genre = Genre.Strategy
                },
                new GenreNode
                {
                    Genre = Genre.Arcade
                },
                new GenreNode
                {
                    Genre = Genre.RPG
                },
                new GenreNode
                {
                    Genre = Genre.Puzzle
                },
                new GenreNode
                {
                    Genre = Genre.Adventure
                },
                new GenreNode
                {
                    Genre = Genre.Action
                },
                new GenreNode
                {
                    Genre = Genre.Races
                },
                new GenreNode
                {
                    Genre = Genre.Sports
                },
                new GenreNode
                {
                    Genre = Genre.Other
                }
            };

            genresData.ForEach(async g => await _gsUnitOfWork.GenreNodeRepository.AddAsync(g));
            _gsUnitOfWork.SaveAsync().Wait();

            var sGenres = new List<GenreNode>
            {
                new GenreNode
                {
                    ParentId = genresData.First(gNode => gNode.Genre == Genre.Races).Id,
                    Genre = Genre.Formula
                },
                new GenreNode
                {
                    ParentId = genresData.First(gNode => gNode.Genre == Genre.Races).Id,
                    Genre = Genre.Off_road
                },
                new GenreNode
                {
                    ParentId = genresData.First(gNode => gNode.Genre == Genre.Races).Id,
                    Genre = Genre.Rally
                },
                new GenreNode
                {
                    ParentId = genresData.First(gNode => gNode.Genre == Genre.RPG).Id,
                    Genre = Genre.MMORPG
                }
            };

            sGenres.ForEach(g => _gsUnitOfWork.GenreNodeRepository.AddAsync(g).Wait());
            _gsUnitOfWork.SaveAsync().Wait();
        }

        private void EnsurePopulatedWithDemoData(GSUnitOfWork _gsUnitOfWork)
        {
            var games = _gsUnitOfWork.GameRepository.GetAllAsync().Result;

            if (games.Any())
            {
                return;
            }

            var gamesData = new List<Game>
            {
                new Game
                {
                    Title = "World Of Warcraft",
                    Genres = new List<Genre>
                    {
                        Genre.RPG,
                        Genre.Strategy
                    },
                    Price = 42,
                    Description = "WOW Game"
                },

                new Game
                {
                    Title = "Prince Of Persia",
                    Genres = new List<Genre>
                    {
                        Genre.Action,
                        Genre.Adventure
                    },
                    Price = 34,
                    Description = "Sand Of Time"
                },

                new Game
                {
                    Title = "Call Of Duty: MW3",
                    Genres = new List<Genre> { Genre.Action },
                    Price = 34,
                    Description = "Captain Price"
                }
            };

            gamesData.ForEach(async g => await _gsUnitOfWork.GameRepository.AddAsync(g));
            _gsUnitOfWork.SaveAsync();
        }
    }
}
