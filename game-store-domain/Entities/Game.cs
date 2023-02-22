using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Entities
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        public byte[] Image { get; set; }

        virtual public List<Genre> Genres { get; set; } = new List<Genre>();

        public void CopyFrom(Game game)
        {
            if (Title != game.Title)
            {
                Title = game.Title;
            }

            if (Description != game.Description)
            {
                Description = game.Description;
            }

            if (Price != game.Price)
            {
                Price = game.Price;
            }

            if (game.Genres != null && !game.Genres.SequenceEqual(Genres))
            {
                Genres = game.Genres;
            }

            if (Genres != game.Genres)
            {
                Genres = game.Genres;
            }

            if (Image != game.Image)
            {
                Image = game.Image;
            }
        }
    }
}
