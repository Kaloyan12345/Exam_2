using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MaxLength(20)]
        public string LastName { get; set; }

        [Required, Range(10, 80)]
        public int Age { get; set; }

        [Required, MaxLength(20)]
        public string Username { get; set; }

        [Required, MaxLength(70)]
        public string Password { get; set; }

        [Required, MaxLength(20)]
        public string Email { get; set; }

        public List<User> Friends { get; set; }

        public List<Game> Games { get; set; }

        private User()
        {
            
        }

        public User(string firstName, string lastName, int age, string username, string password, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Username = username;
            Password = password;
            Email = email;
            Friends = new List<User>();
            Games = new List<Game>();
        }
    }
}
