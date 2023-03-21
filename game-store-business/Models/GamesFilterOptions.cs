namespace game_store_business.Models
{
    public class GamesFilterOptions
    {
        public ICollection<int>? AppliedGenres { get; set; }
        public string? TitleSubstring { get; set; }
    }
}
