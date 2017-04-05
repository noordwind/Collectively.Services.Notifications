using System;
using System.Collections.Generic;
using Collectively.Common.Domain;

namespace Collectively.Services.Notifications.Domain
{
    public class RemarkSubscribers : IdentifiableEntity
    {
        public Guid RemarkId { get; protected set; }
        public ISet<Guid> Users { get; protected set; }

        public RemarkSubscribers(Guid remarkId)
        {
            RemarkId = remarkId;
            Users = new HashSet<Guid>();
        }

        public void AddSubscriber(Guid userId)
        {
            Users.Add(userId);
        }
    }
}