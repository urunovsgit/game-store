using game_store_domain.Entities;
using game_store_domain.Services.Infrastrucure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain
{
    public class GameStoreDbContext : IdentityDbContext<GameStoreUser>
    {
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GenreNode> GenreNodes { get; set; }

        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameStoreUser>()
                .Property(user => user.AvatarImage)
                .IsRequired(false);

            modelBuilder.Entity<Game>()
                .Property(game => game.Genres)
                .HasConversion(new GameGenreConverter())
                .IsRequired(false);

            modelBuilder.Entity<GenreNode>(entity =>
            {
                entity.HasKey(node => node.Id);
                entity.Property(node => node.Genre);
                entity.HasOne(x => x.ParentGenre)
                    .WithMany(x => x.SubGenres)
                    .HasForeignKey(x => x.ParentId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<GenreNode>()
                .HasIndex(gNode => gNode.Genre)
                .IsUnique();
        }
    }
}
