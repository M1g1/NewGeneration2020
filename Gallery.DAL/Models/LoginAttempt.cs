﻿using System;

namespace Gallery.DAL.Models
{
    public class LoginAttempt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public bool IsSuccess { get; set; }
        public string IpAddress { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}