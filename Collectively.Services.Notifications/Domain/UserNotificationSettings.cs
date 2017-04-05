using System;
using Collectively.Common.Domain;

namespace Collectively.Services.Notifications.Domain
{
    public class UserNotificationSettings : IdentifiableEntity
    {
        public Guid UserId { get; set; }
        public bool EmailEnabled { get; set; }
        public NotificationSettings EmailSettings { get; set; }
        public bool PushEnabled { get; set; }
        public NotificationSettings PushSettings { get; set; }
    }
}