using Azure;
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
    public static class GameServiceExtensions
    {
        public static IQueryable<Game> OrderAndFilterBy(this IQueryable<Game> queryable, SortFilterPageOptions options)
        {
            switch(options.OrderOptions)
            {
                case GameOption.Title:
                    queryable = queryable.OrderBy(game => game.Title); break;
                case GameOption.Price:
                    queryable = queryable.OrderBy(game => game.Price); break;
                case GameOption.Date:
                    queryable = queryable.OrderBy(game => game.PublishDate); break;
                case GameOption.Genre:
                    queryable = queryable.OrderBy(game => game.Genres); break;
                default:
                    queryable = queryable.OrderBy(game => game.Id); break;
            }

            /*switch (options.FilterOption)
            {
                case GameOption.Title:
                    queryable = queryable.OrderBy(game => game.Title); break;
                case GameOption.Price:
                    queryable = queryable.OrderBy(game => game.Price); break;
                case GameOption.Date:
                    queryable = queryable.OrderBy(game => game.PublishDate); break;
                case GameOption.Genre:
                    queryable = queryable.OrderBy(game => game.Genres); break;
                case GameOption.Owner:
                    queryable = queryable.OrderBy(game => game.Owner); break;
                default:
                    queryable = queryable.OrderBy(game => game.Id); break;
            }*/

            return queryable;
        }

        public static IQueryable<Game> Paginate(this IQueryable<Game> queryable, SortFilterPageOptions options)
        {
            //var totalPages = (int)Math.Ceiling((decimal)queryable.Count() / options.PageSize);

            //if (options.Page > totalPages)
            //{
            //    options.Page = totalPages;
            //}
            //else if (options.Page == 0)
            //{
            //    options.Page = 1;
            //}

            return queryable.Skip((options.Page - 1) * options.PageSize)
                            .Take(options.PageSize);
        }
    }
}
