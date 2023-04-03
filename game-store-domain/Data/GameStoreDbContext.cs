using game_store_domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace game_store_domain.Data
{
    public class GameStoreDbContext : IdentityDbContext<GameStoreUser, IdentityRole<int>, int>
    {
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GenreNode> GenreNodes { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }


        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
        {
            Database.EnsureCreatedGameGenres(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameStoreUser>(entity =>
            {
                entity.Property(user => user.AvatarImage)
                    .IsRequired(false);

                entity.HasOne(user => user.Cart)
                    .WithOne(cart => cart.User)
                    .HasForeignKey<GameStoreUser>(user => user.CartId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);
            });
                

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(game => game.Genres)
                    .HasConversion(new GameGenreConverter())
                    .IsRequired(false);

                entity.Property(game => game.Image)
                    .IsRequired(false);               
            });


            modelBuilder.Entity<GenreNode>(entity =>
            {
                entity.Property(genre => genre.Genre)
                    .IsRequired(true);

                entity.HasIndex(gNode => gNode.Genre)
                    .IsUnique();

                entity.HasOne(x => x.ParentGenre)
                    .WithMany(x => x.SubGenres)
                    .HasForeignKey(x => x.ParentId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(node => node.Text)
                    .IsRequired(true);

                entity.HasOne(x => x.RelatedTo)
                    .WithMany(x => x.SubComments)
                    .HasForeignKey(x => x.ParentId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(x => x.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Game)
                    .WithMany(g => g.Comments)
                    .HasForeignKey(x => x.GameId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(node => node.Id);

                entity.HasOne(x => x.User)
                    .WithMany(x => x.Orders)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(node => node.Id);

                entity.HasOne(x => x.User)
                    .WithOne(x => x.Cart)
                    .HasForeignKey<Cart>(x => x.UserId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Order)
                    .WithOne(x => x.Cart)
                    .HasForeignKey<Cart>(x => x.OrderId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(node => node.Id);
                entity.Property(x => x.Quantity)
                    .IsRequired(true);

                entity.HasOne(x => x.Game)
                    .WithMany(x => x.CartItems)
                    .HasForeignKey(x => x.GameId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Cart)
                    .WithMany(x => x.Items)
                    .HasForeignKey(x => x.CartId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
