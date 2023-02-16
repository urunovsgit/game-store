using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Entities
{
    public enum Genre
    {
        [Display(Name = "Strategy")]
        Strategy,

        [Display(Name = "RPG")]
        RPG,

        [Display(Name = "Sports")]
        Sports,

        [Display(Name = "Races")]
        Races,

        [Display(Name = "Action")]
        Action,

        [Display(Name = "Adventure")]
        Adventure,

        [Display(Name = "Puzzle")]
        Puzzle,

        [Display(Name = "MMORPG")]
        MMORPG,

        [Display(Name = "Rally")]
        Rally,

        [Display(Name = "Arcade")]
        Arcade,

        [Display(Name = "Formula")]
        Formula,

        [Display(Name = "Off-road")]
        Off_road,

        [Display(Name = "Other")]
        Other
    }

    public class GenreNode
    {
        public int Id { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [AllowNull]
        virtual public GenreNode ParentGenre { get; set; }

        virtual public int? ParentId { get; set; }

        [AllowNull]
        virtual public List<GenreNode> SubGenres { get; set; }
    }
}
