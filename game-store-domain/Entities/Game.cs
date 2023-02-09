using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Entities
{
    public enum GameGenres
    {
        Other,
        Strategy,
        RPG,
        Sports,
        Races,
        Action,
        Adventure,
        Puzzle_and_skill
    }

    internal class Game
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get;set; }

    }
}
