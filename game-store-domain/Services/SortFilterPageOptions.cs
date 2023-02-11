using game_store_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Services
{
    public enum GameOption
    {
        Title, Price, Date, Genre, Owner
    }

    public class SortFilterPageOptions
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GameOption FilterOption { get;set; }
        public GameOption OrderOptions { get; set; }
        public object FilterValue { get; set; }
    }
}
