using AutoMapper;
using game_store_business.Models;
using game_store_domain.Entities;
using System.Text;

namespace Business
{
    public class GSMapperProfile : Profile
    {
        public GSMapperProfile()
        {
            CreateMap<GameModel, Game>()
                .ForMember(g => g.Genres, opt => opt.MapFrom(gm => gm.Genres.Any()
                                                                    ? gm.Genres
                                                                    : new List<Genre> { Genre.Other }))
                .ReverseMap()
                .ForMember(gm => gm.CommentModels, opt => opt.MapFrom(g => g.Comments.Where(c => !c.IsDeleted)))
                .ForMember(gm => gm.Genres, opt => opt.MapFrom(g => g.Genres));

            CreateMap<CommentModel, Comment>()
                .ReverseMap()
                .ForMember(cm => cm.SubCommentsIds, opt => opt.MapFrom(c => c.SubComments.Select(sc => sc.Id)))
                .ForMember(cm => cm.Username, opt => opt.MapFrom(c => c.User.UserName));

            CreateMap<CartItemModel, CartItem>()
                .ReverseMap()
                .ForMember(cim => cim.GameTitle, opt => opt.MapFrom(ci => ci.Game.Title))
                .ForMember(cim => cim.GameImage, opt => opt.MapFrom(ci => ci.Game.Image))
                .ForMember(cim => cim.GamePrice, opt => opt.MapFrom(ci => ci.Game.Price));

            CreateMap<Cart, CartModel>()
                .ForMember(cm => cm.Items, opt => opt.MapFrom(c => c.Items))
                .ReverseMap();

            CreateMap<Order, OrderModel>()
                .ReverseMap();

            CreateMap<GenreNode, GenreNodeModel>()
                .ReverseMap();

            CreateMap<GameStoreUser, GameStoreUserModel>()
                .ForMember(um => um.AvatarImage, opt => opt.MapFrom(u => Convert.ToBase64String(u.AvatarImage)))
                .ForMember(um => um.CommentsIds, opt => opt.MapFrom(u => u.Comments.Select(c => c.Id)))
                .ForMember(um => um.OrdersIds, opt => opt.MapFrom(u => u.Orders.Select(o => o.Id)))
                .ReverseMap();
        }
    }
}