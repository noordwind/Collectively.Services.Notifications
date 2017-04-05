using Collectively.Common.Domain;

namespace Collectively.Services.Notifications.Domain
{
    public class UserNotificationSettings : IdentifiableEntity
    {
        public string UserId { get; set; }
        public NotificationSettings EmailSettings { get; set; }
        public NotificationSettings PushSettings { get; set; }
    }
}