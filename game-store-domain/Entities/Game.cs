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

        [Required]
        public string Owner { get; set; }

        [Required]
        virtual public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
