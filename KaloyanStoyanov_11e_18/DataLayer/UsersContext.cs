using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class UsersContext : IDb<User, int>
    {
        GamingDbContext dbContext;

        public UsersContext(GamingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(User item)
        {
            dbContext.Users.Add(item);
            dbContext.SaveChanges();
        }

        public User Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<User> query = dbContext.Users;

            if (useNavigationalProperties) query = query
                    .Include(f => f.Friends)
                    .Include(g => g.Games);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            User user = query.FirstOrDefault(g => g.Id == key);

            if (user is null) throw new ArgumentException($"User with id = {key} does not exist!");

            return user;
        }

        public List<User> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<User> query = dbContext.Users;

            if (useNavigationalProperties) query = query
                    .Include(f => f.Friends)
                    .Include(g => g.Games);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            return query.ToList();
        }

        public void Update(User item, bool useNavigationalProperties)
        {
            User userFromDb = Read(item.Id, useNavigationalProperties);

            dbContext.Entry<User>(userFromDb).CurrentValues.SetValues(item);

            if (useNavigationalProperties)
            {
                List<User> friends = new List<User>(item.Friends.Count);
                for (int i = 0; i < item.Friends.Count; ++i)
                {
                    User friendFromDb = dbContext.Users.Find(item.Friends[i].Id);
                    if (friendFromDb != null) friends.Add(friendFromDb);
                    else friends.Add(item.Friends[i]);
                }
                userFromDb.Friends = friends;

                List<Game> games = new List<Game>(item.Games.Count);
                for (int i = 0; i < item.Games.Count; ++i)
                {
                    Game gameFromDb = dbContext.Games.Find(item.Games[i].Id);
                    if (gameFromDb != null) games.Add(gameFromDb);
                    else games.Add(item.Games[i]);
                }
                userFromDb.Games = games;
            }

            dbContext.SaveChanges();
        }
        public void Delete(int key)
        {
            User user = Read(key);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }
    }
}
