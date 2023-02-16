using game_store_domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Services.Infrastrucure
{
    public class GameGenreConverter : ValueConverter<List<Genre>, string>
    {
        public GameGenreConverter() : base(le => GenresToString(le), (s => StringToGenres(s)))
        {

        }

        public static string GenresToString(List<Genre> genres)
        {
            if (genres == null)
            {
                return null;
            }

            string genresAsString = "";
            genres.ForEach(gen => { genresAsString += (int)gen + " "; });

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
