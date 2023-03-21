﻿using game_store.Infrastructure;
using game_store_business.Models;
using game_store_domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace game_store.Models
{
    public class GameViewModelBase : GameModel
    {
        public GameViewModelBase(GameModel model)
        {
            Id = model.Id;
            Title = model.Title;
            Price = model.Price;
            Description = model.Description;
            Image = model.Image;
            Genres = model.Genres;
        }

        public string GameImageUrl
        {
            get
            {
                if (Image != null && Image.Length != 0)
                {
                    return string.Format("data:image/jpg;base64,{0}", Image);
                }
                else
                {
                    return "/img/default-game-image.jpeg";
                }
            }
        }

        public string GenresAsString
        {
            get
            {
                string genresAsString = "";

                foreach (var genre in Genres)
                {
                    var genreName = genre.GetAttribute<DisplayAttribute>().Name;
                    genresAsString += string.IsNullOrEmpty(genresAsString)
                                                    ? genresAsString += genreName
                                                    : genresAsString += " / " + genreName;
                }

                return genresAsString;
            }
        }
    }
}
