using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class GamesContext : IDb<Game, int>
    {
        GamingDbContext dbContext;

        public GamesContext(GamingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Game item)
        {
            dbContext.Games.Add(item);
            dbContext.SaveChanges();
        }

        public Game Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Game> query = dbContext.Games;

            if (useNavigationalProperties) query = query
                    .Include(g => g.GameGenres)
                    .Include(u => u.Users);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            Game game = query.FirstOrDefault(g => g.Id == key);

            if (game is null) throw new ArgumentException($"Game with id = {key} does not exist!");

            return game;
        }

        public List<Game> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Game> query = dbContext.Games;

            if (useNavigationalProperties) query = query
                    .Include(g => g.GameGenres)
                    .Include(u => u.Users);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            return query.ToList();
        }

        public void Update(Game item, bool useNavigationalProperties = false)
        {
            Game gameFromDb = Read(item.Id, useNavigationalProperties);

            dbContext.Entry<Game>(gameFromDb).CurrentValues.SetValues(item);

            if (useNavigationalProperties)
            {
                List<User> users = new List<User>(item.Users.Count);
                for (int i = 0; i < item.Users.Count; ++i)
                {
                    User userFromDb = dbContext.Users.Find(item.Users[i].Id);
                    if (userFromDb != null) users.Add(userFromDb);
                    else users.Add(item.Users[i]);
                }
                gameFromDb.Users = users;

                List<GameGenre> gameGenres = new List<GameGenre>(item.GameGenres.Count);
                for (int i = 0; i < item.GameGenres.Count; ++i)
                {
                    GameGenre gameGenreFromDb = dbContext.GameGenres.Find(item.GameGenres[i].Id);
                    if (gameGenreFromDb != null) gameGenres.Add(gameGenreFromDb);
                    else gameGenres.Add(item.GameGenres[i]);
                }
                gameFromDb.GameGenres = gameGenres;
            }

            dbContext.SaveChanges();
        }

        public void Delete(int key)
        {
            Game game = Read(key);
            dbContext.Games.Remove(game);
            dbContext.SaveChanges();
        }
    }
}
