using System;
using System.Collections.Generic;
using Collectively.Common.Domain;

namespace Collectively.Services.Notifications.Domain
{
    public class RemarkSubscribers : IdentifiableEntity
    {
        public Guid RemarkId { get; protected set; }
        public ISet<string> Users { get; protected set; }

        public RemarkSubscribers(Guid remarkId)
        {
            RemarkId = remarkId;
            Users = new HashSet<string>();
        }

        public void AddSubscriber(string userId)
        {
            Users.Add(userId);
        }

        public void RemoveSubscriber(string userId)
        {
            Users.Remove(userId);
        }
    }
}