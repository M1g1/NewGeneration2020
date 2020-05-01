namespace Gallery.DAL.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string PathToMedia { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public int? MediaTypeId { get; set; }
        public MediaType Type { get; set; }
    }
}