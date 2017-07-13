using System;
using System.Collections.Generic;
using Structure.Sketching.Formats.Gif.Format;

namespace Collectively.Services.Notifications.Models
{
    public class Remark
    {
        public Guid Id { get; set; }
        public RemarkUser Author { get; set; }
        public RemarkCategory Category { get; set; }
        public Location Location { get; set; }
        public RemarkState State { get; set; }
        public string SmallPhotoUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Rating { get; set; }
        public bool Resolved { get; set; }
        public int CommentsCount { get; set; }
        public int ParticipantsCount { get; set; }
        public IList<File> Photos { get; set; }
        public IList<RemarkState> States { get; set; }
        public ISet<string> Tags { get; set; }
        public IList<Vote> Votes { get; set; }
        public IList<Comment> Comments { get; set; }
        public ISet<string> UserFavorites { get; set; }
        public ISet<Participant> Participants { get; set; }
    }
}