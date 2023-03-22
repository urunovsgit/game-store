using Business;
using Data.Interfaces;
using game_store.Models;
using game_store_business.Models;
using game_store_business.ServiceInterfaces;
using game_store_business.ServiceProviders;
using game_store_business.Services;
using game_store_business.ServicesProviders;
using game_store_domain.Data;
using game_store_domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace game_store.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddGameStoreServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, GSUnitOfWork>();
            services.AddAutoMapper(Assembly.GetAssembly(typeof(GSMapperProfile)));
            services.AddScoped<IGameService, GameServiceProvider>();
            services.AddScoped<IOrderService, OrderServiceProvider>();
            services.AddScoped<ICommentService, CommentServiceProvider>();

            return services;
        }

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

        public static IEnumerable<GenreNodeViewModel> ToGenreNodeViewModels(
            this IEnumerable<GenreNodeModel> nodes,
            IEnumerable<Genre> appliedGenres)
        {
            var genreModels = new List<GenreNodeViewModel>();
            var parrentNodes = nodes.Where(gnm => gnm.ParentId == null);

            foreach (var node in parrentNodes)
            {
                var subGenres = new Collection<GenreNodeViewModel>();

                if (node.SubGenres.Any())
                {
                    foreach (var sgenre in node.SubGenres)
                    {
                        subGenres.Add(new GenreNodeViewModel
                        {
                            GenreNodeId = sgenre.Id,
                            Genre = sgenre.Genre,
                            GenreName = sgenre.Genre.GetAttribute<DisplayAttribute>().Name,
                            Checked = appliedGenres.Any(g => g == sgenre.Genre)
                        });
                    }
                }

                genreModels.Add(new GenreNodeViewModel
                {
                    GenreNodeId = node.Id,
                    Genre = node.Genre,
                    GenreName = node.Genre.GetAttribute<DisplayAttribute>().Name,
                    Checked = appliedGenres.Any(g => g == node.Genre),
                    SubGenreModels = subGenres
                });
            }

            return genreModels;
        }
    }
}
