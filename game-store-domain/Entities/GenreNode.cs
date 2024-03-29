﻿using System.ComponentModel.DataAnnotations;

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

    public class GenreNode : BaseEntity
    {
        public Genre Genre { get; set; }
        virtual public GenreNode ParentGenre { get; set; }
        virtual public int? ParentId { get; set; }
        virtual public ICollection<GenreNode> SubGenres { get; set; }
    }
}
