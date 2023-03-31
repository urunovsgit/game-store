using game_store_domain.Data;
using game_store_domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace game_store
{
    public class DataSeeder
    {
        public static void Init(WebApplication application)
        {
            using (var scope = application.Services.CreateScope())
            {
                using var dbContext = new GameStoreDbContext(
                    scope.ServiceProvider.GetRequiredService<DbContextOptions<GameStoreDbContext>>());

                EnsurePopulatedWithDemoData(dbContext, application.Environment.WebRootPath);
            }
        }

        private static void EnsurePopulatedWithDemoData(GameStoreDbContext dbContext, string contentPath)
        {
            var gamesSet = dbContext.Set<Game>();

            if (gamesSet.Any()) return;

            var gamesData = new List<Game>
            {
                new Game
                {
                    Title = "World Of Warcraft",
                    Genres = new List<Genre> { Genre.RPG, Genre.Strategy },
                    Price = 42,
                    Description = "You've taken Azeroth and Outland by storm. Now the Lich King’s forces await you in the icy continent of Northrend. Face the undead Scourge and take the fight to the Lich King himself atop Icecrown Citadel.",
                    Image = LoadGameImageBytes(contentPath, "img/seed/wow-lichking.jpg")
                },

                new Game
                {
                    Title = "Call Of Duty: MW3",
                    Genres = new List<Genre> { Genre.Action },
                    Price = 34,
                    Description = "Modern Warfare is back. The best-selling first person action series of all-time returns with the epic sequel to multiple Game of the Year award winner, Call of Duty®: Modern Warfare 2.",
                    Image = LoadGameImageBytes(contentPath, "img/seed/codmw3.jpg")
                },

                new Game
                {
                    Title = "Assassin's Creed III",
                    Genres = new List<Genre> { Genre.Action, Genre.Adventure },
                    Price = 30,
                    Description = "Relive the American Revolution or experience it for the first time in Assassin's Creed III Remastered, with enhanced graphics and improved gameplay mechanics.",
                    Image = LoadGameImageBytes(contentPath, "img/seed/asscreed3.png")
                },

                new Game
                {
                    Title = "Diablo III",
                    Genres = new List<Genre> { Genre.RPG },
                    Price = 27,
                    Description = "Diablo III is an action role-playing game that takes place throughout the dark fantasy world of Sanctuary. You play a hero who engages in fast-paced combat that tests your reflexes and rewards tactical decisions.",
                    Image = LoadGameImageBytes(contentPath, "img/seed/diablo3.jpg")
                },

                new Game
                {
                    Title = "Grand Theft Auto V",
                    Genres = new List<Genre> { Genre.Action, Genre.Adventure },
                    Price = 39,
                    Description = "When a young street hustler, a retired bank robber, and a terrifying psychopath find themselves entangled with some of the most frightening and deranged elements of the criminal underworld, the U.S. government.",
                    Image = LoadGameImageBytes(contentPath, "img/seed/gtaV.jpg")
                },

                new Game
                {
                    Title = "Half-Life 2",
                    Genres = new List<Genre> { Genre.Action, Genre.Other},
                    Price = 24,
                    Description = "Half-Life 2 is a 2004 first-person shooter (FPS) game developed by Valve. It was published by Valve through its distribution service Steam. Like the original Half-Life (1998), Half-Life 2 combines shooting, puzzles, and storytelling, and adds features such as vehicles and physics-based gameplay.",
                    Image = LoadGameImageBytes(contentPath, "img/seed/hl2.jpg")
                },

                new Game
                {
                    Title = "Minecraft",
                    Genres = new List<Genre> { Genre.Other},
                    Price = 29,
                    Description = "Explore your own unique world, survive the night, and create anything you can imagine!",
                    Image = LoadGameImageBytes(contentPath, "img/seed/minecraft.jpg")
                },

                new Game
                {
                    Title = "Portal 2",
                    Genres = new List<Genre> { Genre.Puzzle, Genre.Other},
                    Price = 25,
                    Description = "The \"Perpetual Testing Initiative\" has been expanded to allow you to design co-op puzzles for you and your friends!",
                    Image = LoadGameImageBytes(contentPath, "img/seed/portal2.jpg")
                },

                new Game
                {
                    Title = "Quake",
                    Genres = new List<Genre> { Genre.Arcade, Genre.Other},
                    Price = 19,
                    Description = "Developed by the award-winning id Software, Quake® is the ground-breaking, original dark fantasy first-person shooter that inspires today’s retro-style FPS games. With Quake (Enhanced), experience the authentic, updated, and visually enhanced version of the original.",
                    Image = LoadGameImageBytes(contentPath, "img/seed/quake.jpg")
                },

                new Game
                {
                    Title = "Resident Evil Village",
                    Genres = new List<Genre> { Genre.Other },
                    Price = 36,
                    Description = "The next generation of survival horror rises in the form of Resident Evil Village, the eighth major entry in the Resident Evil series. With ultra-realistic graphics powered by the RE Engine, fight for survival as danger lurks around every corner.",
                    Image = LoadGameImageBytes(contentPath, "img/seed/resevil.jpg")
                }
            };

            gamesData.ForEach(game => gamesSet.Add(game));
            dbContext.SaveChanges();
        }

        private static byte[] LoadGameImageBytes(string rootPath, string imageLocalPath)
        {
            var filepath = Path.Combine(rootPath, imageLocalPath);
            return File.ReadAllBytes(filepath);
        }
    }
}
