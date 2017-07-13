using Collectively.Services.Notifications.Domain;

namespace Collectively.Services.Notifications.Dto
{
    public class UserNotificationSettingsDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Culture { get; set; }
        public NotificationSettings EmailSettings { get; set; }
        public NotificationSettings PushSettings { get; set; }
    }
}