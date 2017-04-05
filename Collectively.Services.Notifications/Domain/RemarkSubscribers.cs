using System.Collections.Generic;
using Collectively.Common.Domain;

namespace Collectively.Services.Notifications.Domain
{
    public class RemarkSubscribers : IdentifiableEntity
    {
        public string RemarkId { get; protected set; }
        public IList<string> Users { get; protected set; }

        public RemarkSubscribers(string remarkId)
        {
            RemarkId = remarkId;
            Users = new List<string>();
        }

        public void AddSubscriber(string userId)
        {
            Users.Add(userId);
        }
    }
}