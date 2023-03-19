using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace game_store_domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Text{ get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        virtual public GameStoreUser User { get; set; }
        virtual public string UserId { get; set; }
        virtual public Game Game { get; set; }
        virtual public int GameId { get; set; }
        virtual public int? ParentId { get; set; }
        virtual public Comment RelatedTo { get; set; }
        virtual public List<Comment> SubComments { get; set; }
    }
}
