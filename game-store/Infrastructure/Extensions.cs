using game_store_domain;
using game_store_domain.Entities;
using System;

namespace game_store.Infrastructure
{
    public static class Extensions
    {
        public static void EnsurePopulatedWithDemoData(this GameStoreDbContext appDbContext)
        {
            if (!appDbContext.Set<Game>().Any())
            {
                appDbContext.Set<Game>().AddRange(
                    new Game
                    {
                        Title = "World Of Warcraft",
                        Genres = new List<Genre> { new Genre { Name = "RPG"}, new Genre { Name = "Strategy" } },
                        Owner = "Blizzard",
                        Price = 42,
                        Description = "WOW Game"
                    },

                    new Game
                    {
                        Title = "Prince Of Persia",
                        Genres = new List<Genre> { new Genre { Name = "Action" }, new Genre { Name = "Adventure" } },
                        Owner = "Ubisoft",
                        Price = 34,
                        Description = "Sand Of Time"
                    },

                    new Game
                    {
                        Title = "Call Of Duty: MW3",
                        Genres = new List<Genre> { new Genre { Name = "Action" } },
                        Owner = "Activision",
                        Price = 34,
                        Description = "Captain Price"
                    }
                );

                appDbContext.SaveChanges();
            }
        }
    }
}
