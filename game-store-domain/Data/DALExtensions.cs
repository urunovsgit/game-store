using game_store_domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_domain.Data
{
    public static class DALExtensions
    {
        public static void EnsureCreatedGameGenres(this DatabaseFacade database, GameStoreDbContext dbContext)
        {
            database.EnsureCreated();
            var genreNodesSet = dbContext.Set<GenreNode>();

            if (genreNodesSet.Any()) return;

            var genresData = new List<GenreNode>
            {
                new GenreNode
                {
                    Genre = Genre.Strategy
                },
                new GenreNode
                {
                    Genre = Genre.Arcade
                },
                new GenreNode
                {
                    Genre = Genre.RPG
                },
                new GenreNode
                {
                    Genre = Genre.Puzzle
                },
                new GenreNode
                {
                    Genre = Genre.Adventure
                },
                new GenreNode
                {
                    Genre = Genre.Action
                },
                new GenreNode
                {
                    Genre = Genre.Races
                },
                new GenreNode
                {
                    Genre = Genre.Sports
                },
                new GenreNode
                {
                    Genre = Genre.Other
                }
            };

            genresData.ForEach(g => genreNodesSet.Add(g));
            dbContext.SaveChanges();

            var subGenres = new List<GenreNode>
            {
                new GenreNode
                {
                    ParentId = genresData.First(gNode => gNode.Genre == Genre.Races).Id,
                    Genre = Genre.Formula
                },
                new GenreNode
                {
                    ParentId = genresData.First(gNode => gNode.Genre == Genre.Races).Id,
                    Genre = Genre.Off_road
                },
                new GenreNode
                {
                    ParentId = genresData.First(gNode => gNode.Genre == Genre.Races).Id,
                    Genre = Genre.Rally
                },
                new GenreNode
                {
                    ParentId = genresData.First(gNode => gNode.Genre == Genre.RPG).Id,
                    Genre = Genre.MMORPG
                }
            };

            subGenres.ForEach(g => genreNodesSet.Add(g));
            dbContext.SaveChanges();
        }
    }
}
