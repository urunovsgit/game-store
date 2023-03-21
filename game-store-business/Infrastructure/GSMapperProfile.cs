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
            CreateMap<Game, GameModel>()
                .ForMember(gm => gm.CommentModels, opt => opt.MapFrom(g => g.Comments))
                .ReverseMap();

            CreateMap<CartItem, CartItemModel>()
                .ReverseMap();

            CreateMap<Cart, CartModel>()
                .ForMember(cm => cm.CartItemsIds, opt => opt.MapFrom(c => c.Items.Select(i => i.Id)))
                .ReverseMap();

            CreateMap<Comment, CommentModel>()
                .ForMember(cm => cm.SubCommentsIds, opt => opt.MapFrom(c => c.SubComments.Select(sc => sc.Id)))
                .ForMember(cm => cm.Username, opt => opt.MapFrom(c => c.User.UserName))
                .ReverseMap();

            CreateMap<GenreNode, GenreNodeModel>()
                //.ForMember(gnm => gnm.SubGenresIds, opt => opt.MapFrom(gn => gn.SubGenres.Select(sg => sg.Id)))
                .ReverseMap();

            CreateMap<GameStoreUser, GameStoreUserModel>()
                .ForMember(um => um.AvatarImage, opt => opt.MapFrom(u => Convert.ToBase64String(u.AvatarImage)))
                .ForMember(um => um.CommentsIds, opt => opt.MapFrom(u => u.Comments.Select(c => c.Id)))
                .ForMember(um => um.OrdersIds, opt => opt.MapFrom(u => u.Orders.Select(o => o.Id)))
                .ReverseMap();

            CreateMap<Order, OrderModel>()
                .ReverseMap();
        }
    }
}