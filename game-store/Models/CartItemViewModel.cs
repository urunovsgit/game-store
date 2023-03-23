using game_store_business.Models;

namespace game_store.Models
{
    public class CartItemViewModel : CartItemModel
    {
        public CartItemViewModel() { }
        public CartItemViewModel(CartItemModel itemModel)
        {
            Id = itemModel.Id;
            Quantity = itemModel.Quantity;
            GameId = itemModel.GameId;
            CartId = itemModel.CartId;
            GameImage = itemModel.GameImage;
            GameTitle = itemModel.GameTitle;
            GamePrice = itemModel.GamePrice;
        }

        public string GameImageUrl
        {
            get
            {
                if (GameImage != null && GameImage.Length != 0)
                {
                    return string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(GameImage));
                }
                else
                {
                    return "/img/default-game-image.jpeg";
                }
            }
        }

        public decimal Sum { get => Quantity * GamePrice; }
    }
}
