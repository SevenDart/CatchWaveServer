using System.Collections.Generic;

namespace BLL.Models
{
    public class Room
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public User Owner { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}