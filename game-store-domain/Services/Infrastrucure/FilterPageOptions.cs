namespace game_store_domain.Services.Infrastrucure
{
    public enum GameOption
    {
        None, Title, Price, Date, Genre, Owner
    }

    public struct GameFilterUnit
    {
        public GameOption filterOption { get; set; }
        public object FilterValue { get; set; }
    }

    public class FilterPageOptions
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GameOption OrderOption { get; set; }
        public Dictionary<GameOption, object> FilterValueUnits { get; set; } =
            new Dictionary<GameOption, object> { { GameOption.None, new object() } };
    }
}
