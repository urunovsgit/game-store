namespace game_store_business.Models
{
    public class FilterOptionsModel
    {
        public ICollection<int>? AppliedGenresIds { get; set; }
        public string? TitleSubstring { get; set; }
    }
}
