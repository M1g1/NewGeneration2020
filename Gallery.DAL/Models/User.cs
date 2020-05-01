using System.Collections.Generic;

namespace Gallery.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
        public List<Media> Media { get; set; }

        public User()
        {
            Roles = new List<Role>();
            Media = new List<Media>();
        }
    }
}
