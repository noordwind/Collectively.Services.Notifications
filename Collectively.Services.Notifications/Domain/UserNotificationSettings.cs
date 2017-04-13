using Collectively.Common.Domain;

namespace Collectively.Services.Notifications.Domain
{
    public class UserNotificationSettings : IdentifiableEntity
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Culture { get; set; }
        public NotificationSettings EmailSettings { get; set; }
        public NotificationSettings PushSettings { get; set; }

        public void Update(UserNotificationSettings userSettings)
        {
            UserId = userSettings.UserId;
            Email = userSettings.Email;
            Username = userSettings.Username;
            Culture = userSettings.Culture;
            EmailSettings = userSettings.EmailSettings;
            PushSettings = userSettings.PushSettings;
        }

        public static UserNotificationSettings Create(string userId, 
            string email, string name, string culture)
            => new UserNotificationSettings
            {
                UserId = userId,
                Email = email,
                Username = name,
                Culture = culture,
                EmailSettings = NotificationSettings.Default,
                PushSettings = NotificationSettings.Default
            };
    }
}