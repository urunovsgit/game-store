using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Text{ get; set; }

        [Required]
        public DateTime DateTime { get; set; } = DateTime.Now;

        [Required]
        virtual public GameStoreUser User { get; set; }

        virtual public string UserId { get; set; }

        [Required]
        virtual public Game Game { get; set; }

        virtual public int GameId { get; set; }


        [AllowNull]
        virtual public Comment RelatedTo { get; set; }

        virtual public int? ParentId { get; set; }

        [AllowNull]
        virtual public List<Comment> SubComments { get; set; }
    }
}
