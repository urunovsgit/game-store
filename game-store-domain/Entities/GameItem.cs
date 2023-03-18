using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Entities
{
    public class GameItem
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int Quantity { get; set; } = 1;
        virtual public Game Game { get; set; }

        [NotMapped]
        public decimal Sum
        {
            get { return Quantity * Game.Price; }
        }

        public GameItem() { }

        public GameItem(Game game)
        {
            Game = game;
            GameId = game.Id;
        }
    }
}
