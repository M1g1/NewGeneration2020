using System.Collections.Generic;

namespace Gallery.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();
        public virtual ICollection<Media> Media { get; set; } = new HashSet<Media>();
        public virtual ICollection<MediaUploadAttempt> MediaUploadAttempts { get; set; } = new HashSet<MediaUploadAttempt>();
        public virtual ICollection<LoginAttempt> LoginAttempts { get; set; } = new HashSet<LoginAttempt>();
    }
}
