using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [AllowNull]
        virtual public List<Genre> SubGenres { get; set; }

        virtual public List<Game> Games { get; set; } = new List<Game>();
    }
}
