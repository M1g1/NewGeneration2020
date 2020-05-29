using System;

namespace Gallery.Service.Contract
{
    public class MediaUploadAttemptDto
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public int UserId { get; set; }
        public bool IsInProgress { get; set; }
        public bool IsSuccess { get; set; }
        public string IpAddress { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}