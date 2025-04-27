using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class GameGenresContext : IDb<GameGenre, int>
    {
        GamingDbContext dbContext;

        public GameGenresContext(GamingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(GameGenre item)
        {
            dbContext.GameGenres.Add(item);
            dbContext.SaveChanges();
        }

        public GameGenre Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<GameGenre> query = dbContext.GameGenres;

            if (useNavigationalProperties) query = query.Include(g => g.Users);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            GameGenre gameGenre = query.FirstOrDefault(g => g.Id == key);

            if (gameGenre is null) throw new ArgumentException($"Game genre with id = {key} does not exist!");

            return gameGenre;
        }

        public List<GameGenre> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<GameGenre> query = dbContext.GameGenres;

            if (useNavigationalProperties) query = query.Include(g => g.Users);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            return query.ToList();
        }

        public void Update(GameGenre item, bool useNavigationalProperties)
        {
            GameGenre gameGenreFromDb = Read(item.Id, useNavigationalProperties);

            dbContext.Entry<GameGenre>(gameGenreFromDb).CurrentValues.SetValues(item);

            if (useNavigationalProperties)
            {
                List<User> users = new List<User>(item.Users.Count);
                for (int i = 0; i < item.Users.Count; ++i)
                {
                    User userFromDb = dbContext.Users.Find(item.Users[i].Id);
                    if (userFromDb != null) users.Add(userFromDb);
                    else users.Add(item.Users[i]);
                }
                gameGenreFromDb.Users = users;
            }

            dbContext.SaveChanges();
        }
        public void Delete(int key)
        {
            GameGenre gameGenre = Read(key);
            dbContext.GameGenres.Remove(gameGenre);
            dbContext.SaveChanges();
        }
    }
}
