using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        public List<User> Users { get; set; }

        public List<GameGenre> GameGenres { get; set; }

        private Game()
        {
            
        }

        public Game(string name)
        {   
            Name = name;
            Users = new List<User>();
            GameGenres = new List<GameGenre>();
        }
    }
}
