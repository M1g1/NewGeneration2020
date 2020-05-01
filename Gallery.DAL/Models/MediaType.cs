using System.Collections.Generic;

namespace Gallery.DAL.Models
{
    public enum EnumMediaType
    {
        Image,
        Video, 
        Sound
    }

    public class MediaType
    {
        public int Id { get; set; }
        public EnumMediaType Type { get; set; }

        public List<Media> Media { get; set; }
        public MediaType()
        {
            Media = new List<Media>();
        }
    }
}