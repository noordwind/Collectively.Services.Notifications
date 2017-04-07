using System;

namespace Collectively.Services.Notifications.Models
{
    public class Vote
    {
        public string UserId { get; set; }
        public bool Positive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}