using game_store_domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain
{
    public class GameStoreDbContext : DbContext
    {
        public virtual DbSet<Game> Games { get; set; }

        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseLazyLoadingProxies();
    }
}
