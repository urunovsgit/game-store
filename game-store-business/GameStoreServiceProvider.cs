using AutoMapper;
using Data.Interfaces;
using game_store_business.Models;
using game_store_domain.Entities;

namespace game_store_domain.Services
{
    public class GameStoreServiceProvider : IGameStoreServices
    {
        private readonly IUnitOfWork _gsUnitOfWork;
        private readonly IMapper _mapperProfile;

        public GameStoreServiceProvider(IUnitOfWork unitOfWork, IMapper mapperProfile)
        {
            _gsUnitOfWork = unitOfWork;
            _mapperProfile = mapperProfile;

            Task.Run(EnsureCreatedGameGenres).Wait();
            Task.Run(EnsurePopulatedWithDemoData).Wait();
        }

        public async Task<CommentModel> AddCommentAsync(CommentModel commentDTO)
        {
            var commentDAO = _mapperProfile.Map<Comment>(commentDTO);

            var instance = await _gsUnitOfWork.CommentRepository.AddAsync(commentDAO);
            await _gsUnitOfWork.SaveAsync();

            return _mapperProfile.Map<CommentModel>(instance);
        }

        public async Task<CartModel> AddGameToCartAsync(int gameId, int cartId)
        {
            var cartItem = new CartItem
            {
                CartId = cartId,
                GameId = gameId
            };

            await _gsUnitOfWork.CartItemRepository.AddAsync(cartItem);
            await _gsUnitOfWork.SaveAsync();

            var cart = await _gsUnitOfWork.CartRepository.GetByIdAsync(cartId);

            return _mapperProfile.Map<CartModel>(cart);
        }

        public async Task<GameModel> AddNewGameAsync(GameModel gameDTO)
        {
            var game = _mapperProfile.Map<Game>(gameDTO);
            game = await _gsUnitOfWork.GameRepository.AddAsync(game);
            await _gsUnitOfWork.SaveAsync();

            return _mapperProfile.Map<GameModel>(game);
        }

        public async Task ConfirmOrderAsync(OrderModel orderDTO)
        {
            var order = _mapperProfile.Map<Order>(orderDTO);
            await _gsUnitOfWork.OrderRepository.AddAsync(order);
            var cart = await _gsUnitOfWork.CartRepository.GetByIdAsync(order.CartId);

            foreach (var item in cart.Items)
            {
                _gsUnitOfWork.CartItemRepository.Delete(item);
            }

            await _gsUnitOfWork.SaveAsync();
        }

        public async Task<CartItemUpdateResponse> DecreaseGameQuantityAsync(int cartItemId)
        {
            var cartItem = await _gsUnitOfWork.CartItemRepository.GetByIdAsync(cartItemId);
            cartItem.Quantity--;
            _gsUnitOfWork.CartItemRepository.Update(cartItem);
            await _gsUnitOfWork.SaveAsync();

            return new CartItemUpdateResponse
            {
                Quantity = cartItem.Quantity,
                ItemSum = cartItem.Quantity * cartItem.Game.Price,
                CartSum = cartItem.Cart.Items.Sum(ci => ci.Quantity * ci.Game.Price)
            };
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _gsUnitOfWork.CommentRepository.DeleteByIdAsync(id);
            await _gsUnitOfWork.SaveAsync();
        }

        public async Task DeleteGameAsync(int id)
        {
            await _gsUnitOfWork.GameRepository.DeleteByIdAsync(id);
            await _gsUnitOfWork.SaveAsync();
        }

        public async Task<CommentModel> EditCommentAsync(CommentModel commentDTO)
        {
            var comment = _mapperProfile.Map<Comment>(commentDTO);
            _gsUnitOfWork.CommentRepository.Update(comment);
            await _gsUnitOfWork.SaveAsync();

            comment = await _gsUnitOfWork.CommentRepository.GetByIdAsync(comment.Id);
            return _mapperProfile.Map<CommentModel>(comment);
        }

        public async Task<IEnumerable<GenreNodeModel>> GetAllGenreNodesModelsAsync()
        {
            var nodes = await _gsUnitOfWork.GenreNodeRepository.GetAllAsync();

            return _mapperProfile.Map<IEnumerable<GenreNodeModel>>(nodes);
        }

        public async Task<GameModel> GetGameByIdAsync(int id)
        {
            var game = await _gsUnitOfWork.GameRepository.GetByIdAsync(id);

            return _mapperProfile.Map<GameModel>(game);
        }

        public async Task<IEnumerable<GameModel>> GetGamesAsync(GamesFilterOptions options)
        {
            var games = await _gsUnitOfWork.GameRepository.GetAllAsync();

            if(options.AppliedGenres != null)
            {
                var genresNodes = await _gsUnitOfWork.GenreNodeRepository.GetAllAsync();
                var genres = genresNodes.Where(gn => options.AppliedGenres.Contains((int)gn.Genre)).Select(gn => gn.Genre);

                games = games.Where(game => game.Genres
                                 .Any(genre => genres
                                    .Contains(genre)))
                             .ToList();
            }

            if(options.TitleSubstring != null)
            {
                games = games.Where(game => game.Title
                                            .Contains(options.TitleSubstring, StringComparison.InvariantCultureIgnoreCase))
                                            .ToList();
            }

            return _mapperProfile.Map<IEnumerable<GameModel>>(games);
        }

        public async Task<CartItemUpdateResponse> IncreaseGameQuantityAsync(int cartItemId)
        {
            var cartItem = await _gsUnitOfWork.CartItemRepository.GetByIdAsync(cartItemId);
            cartItem.Quantity++;
            _gsUnitOfWork.CartItemRepository.Update(cartItem);
            await _gsUnitOfWork.SaveAsync();

            return new CartItemUpdateResponse
            {
                Quantity = cartItem.Quantity,
                ItemSum = cartItem.Quantity * cartItem.Game.Price,
                CartSum = cartItem.Cart.Items.Sum(ci => ci.Quantity * ci.Game.Price)
            };
        }

        public async Task<CartModel> RemoveGameFromCartAsync(int cartId, int itemId)
        {
            await _gsUnitOfWork.CartItemRepository.DeleteByIdAsync(itemId);
            await _gsUnitOfWork.SaveAsync();

            var cart = await _gsUnitOfWork.CartRepository.GetByIdAsync(cartId);
            return _mapperProfile.Map<CartModel>(cart);
        }

        public async Task<CommentModel> RestoreCommentAsync(int id)
        {
            var comment = await _gsUnitOfWork.CommentRepository.GetByIdAsync(id);
            comment.IsDeleted = false;

            _gsUnitOfWork.CommentRepository.Update(comment);
            await _gsUnitOfWork.SaveAsync();

            return _mapperProfile.Map<CommentModel>(comment);
        }

        public async Task<GameModel> UpdateGame(GameModel gameDTO)
        {
            var game = _mapperProfile.Map<Game>(gameDTO);

            _gsUnitOfWork.GameRepository.Update(game);
            await _gsUnitOfWork.SaveAsync();

            game = await _gsUnitOfWork.GameRepository.GetByIdAsync(game.Id);
            return _mapperProfile.Map<GameModel>(game);
        }

        private void EnsureCreatedGameGenres()
        {
            var genres = _gsUnitOfWork.GenreNodeRepository.GetAllAsync().Result;
            if (!genres.Any())
            {
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
        }

        private void EnsurePopulatedWithDemoData()
        {
            var games = _gsUnitOfWork.GameRepository.GetAllAsync().Result;

            if (!games.Any())
            {
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
}
