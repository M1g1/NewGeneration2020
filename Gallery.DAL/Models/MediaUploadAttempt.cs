using System;

namespace Gallery.DAL.Models
{
    public class MediaUploadAttempt
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public bool IsInProgress{ get; set; }
        public bool IsSuccess { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}