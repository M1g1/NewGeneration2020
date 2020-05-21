namespace Gallery.DAL.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int MediaTypeId { get; set; }
        public virtual MediaType Type { get; set; }
    }
}