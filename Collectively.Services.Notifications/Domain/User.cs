using Collectively.Common.Domain;

namespace Collectively.Services.Notifications.Domain
{
    public class User : IdentifiableEntity
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Culture { get; set; }
        public NotificationSettings EmailSettings { get; set; }
        public NotificationSettings PushSettings { get; set; }

        public void Update(User user)
        {
            UserId = user.UserId;
            Email = user.Email;
            Username = user.Username;
            Culture = user.Culture;
            EmailSettings = user.EmailSettings;
            PushSettings = user.PushSettings;
        }
    }
}