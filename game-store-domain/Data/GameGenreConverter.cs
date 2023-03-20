using game_store_domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace game_store_domain.Data
{
    public class GameGenreConverter : ValueConverter<IEnumerable<Genre>, string>
    {
        public GameGenreConverter() : base(le => GenresToString(le), s => StringToGenres(s))
        {

        }

        public static string GenresToString(IEnumerable<Genre> genres)
        {
            if (genres == null)
            {
                return null;
            }

            string genresAsString = "";
            genres.ToList().ForEach(gen => { genresAsString += (int)gen + " "; });

            return genresAsString.TrimEnd();
        }

        public static List<Genre> StringToGenres(string genres)
        {
            if (genres == null || genres == string.Empty)
            {
                return new List<Genre>();
            }

            var genresAsEnum = new List<Genre>(genres.Split(' ').Select(i => (Genre)Convert.ToInt32(i)));

            return genresAsEnum;
        }
    }
}
