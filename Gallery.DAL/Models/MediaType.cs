using System.Collections.Generic;

namespace Gallery.DAL.Models
{
    public class MediaType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Media> Media { get; set; } = new HashSet<Media>();
    }
}