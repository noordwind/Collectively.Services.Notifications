using System;

namespace Collectively.Services.Notifications.Models
{
    public class Participant
    {
        public RemarkUser User { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}