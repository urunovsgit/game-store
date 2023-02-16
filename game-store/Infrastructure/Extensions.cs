using game_store_domain;
using game_store_domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace game_store.Infrastructure
{
    public static class Extensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }

        public static T GetValueFromName<T>(this string name)
            where T : Enum
        {
            var type = typeof(T);

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute))
                    is DisplayAttribute attribute && attribute.Name == name)
                {
                    return (T)field.GetValue(null);
                }

                if (field.Name == name)
                {
                    return (T)field.GetValue(null);
                }
            }

            return default;
        }

        public static void EnsurePopulatedWithDemoData(this GameStoreDbContext appDbContext)
        {
            //if (!appDbContext.Set<Game>().Any())
            //{
            //    appDbContext.Set<Game>().AddRange(
            //        new Game
            //        {
            //            Title = "World Of Warcraft",
            //            Genres = new List<GenreNode> { new GenreNode { Name = "RPG" }, new GenreNode { Name = "Strategy" } },
            //            Price = 42,
            //            Description = "WOW Game"
            //        },

            //        new Game
            //        {
            //            Title = "Prince Of Persia",
            //            Genres = new List<GenreNode> { new GenreNode { Name = "Action" }, new GenreNode { Name = "Adventure" } },
            //            Price = 34,
            //            Description = "Sand Of Time"
            //        },

            //        new Game
            //        {
            //            Title = "Call Of Duty: MW3",
            //            Genres = new List<GenreNode> { new GenreNode { Name = "Action" } },
            //            Price = 34,
            //            Description = "Captain Price"
            //        }
            //    );

            //    appDbContext.SaveChanges();
            //}
        }
    }
}
