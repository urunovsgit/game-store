using game_store_domain.Entities;
using game_store_domain.Services.Infrastrucure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Services
{
    public class GameServiceProvider : IGameServices
    {
        private readonly DbContext _storeDbContext;

        public GameServiceProvider(GameStoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
            EnsureCreatedGameGenres();
            EnsurePopulatedWithDemoData();
        }

        public (IEnumerable<Game>, int) GetGames(SortFilterPageOptions options)
        {
            var gamesQuery = _storeDbContext.Set<Game>().AsQueryable();

            var games = gamesQuery.OrderAndFilterBy(options)
                                  .Paginate(options)
                                  .ToList();

            return (games, gamesQuery.Count());
        }

        public Game GetGameById(int id)
        {
            return _storeDbContext.Set<Game>().Find(id);
        }

        public Game AddNewGame(Game game)
        {
            var gameInstance = _storeDbContext.Set<Game>().Add(game);
            _storeDbContext.SaveChanges();

            return gameInstance.Entity;
        }

        public void DeleteGame(int id)
        {
            var game = _storeDbContext.Set<Game>().Find(id);

            if (game != null)
            {
                _storeDbContext.Set<Game>().Remove(game);
                _storeDbContext.SaveChanges();
            }
        }

        public Game UpdateGame(Game game)
        {
            var gameEntity = _storeDbContext.Set<Game>().Find(game.Id);

            //if(gameEntity != null)
            //{
            //    gameEntity.CopyFrom(game);
            //    _storeDbContext.SaveChanges();
            //}

            return gameEntity;
        }

        public List<GenreNode> GetAllGenreNodes()
        {
            return _storeDbContext.Set<GenreNode>()
                .Include(n => n.ParentGenre)
                .ToList();
        }

        private void EnsureCreatedGameGenres()
        {
            if (!_storeDbContext.Set<GenreNode>().Any())
            {
                _storeDbContext.Set<GenreNode>().AddRange(
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
                );

                _storeDbContext.SaveChanges();

                _storeDbContext.Set<GenreNode>().AddRange(
                    new GenreNode
                    {
                        ParentGenre = _storeDbContext.Set<GenreNode>().First(gNode => gNode.Genre == Genre.Races),
                        Genre = Genre.Formula
                    },
                    new GenreNode
                    {
                        ParentGenre = _storeDbContext.Set<GenreNode>().First(gNode => gNode.Genre == Genre.Races),
                        Genre = Genre.Off_road
                    },
                    new GenreNode
                    {
                        ParentGenre = _storeDbContext.Set<GenreNode>().First(gNode => gNode.Genre == Genre.Races),
                        Genre = Genre.Rally
                    },
                    new GenreNode
                    {
                        ParentGenre = _storeDbContext.Set<GenreNode>().First(gNode => gNode.Genre == Genre.RPG),
                        Genre = Genre.MMORPG
                    }
                );

                _storeDbContext.SaveChanges();

                _storeDbContext.Set<GenreNode>().First(gNode => gNode.Genre == Genre.Races)
                    .SubGenres = new List<GenreNode>
                    {
                        _storeDbContext.Set<GenreNode>().First(gNode => gNode.Genre == Genre.Formula),
                        _storeDbContext.Set<GenreNode>().First(gNode => gNode.Genre == Genre.Off_road),
                        _storeDbContext.Set<GenreNode>().First(gNode => gNode.Genre == Genre.Rally)
                    };

                _storeDbContext.Set<GenreNode>().First(gNode => gNode.Genre == Genre.RPG)
                    .SubGenres = new List<GenreNode>
                    {
                        _storeDbContext.Set<GenreNode>().First(gNode => gNode.Genre == Genre.MMORPG)
                    };

                _storeDbContext.SaveChanges();
            }
        }

        private void EnsurePopulatedWithDemoData()
        {
            if (!_storeDbContext.Set<Game>().Any())
            {
                _storeDbContext.Set<Game>().AddRange(
                    new Game
                    {
                        Title = "World Of Warcraft",
                        Genres = new List<Genre> { Genre.RPG, Genre.Strategy },
                        Price = 42,
                        Description = "WOW Game"
                    },

                    new Game
                    {
                        Title = "Prince Of Persia",
                        Genres = new List<Genre> { Genre.Action, Genre.Adventure },
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
                );

                _storeDbContext.SaveChanges();
            }
        }
    }
}
