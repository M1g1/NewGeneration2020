using System;

namespace Gallery.DAL.Models
{
    public class Attempt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsSuccess { get; set; }
        public string IpAddress { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}