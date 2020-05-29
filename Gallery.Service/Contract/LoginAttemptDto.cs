using System;

namespace Gallery.Service.Contract
{
    public class LoginAttemptDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsSuccess { get; set; }
        public string IpAddress { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}