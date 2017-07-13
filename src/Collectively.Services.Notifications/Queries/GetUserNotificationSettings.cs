using Collectively.Common.Queries;

namespace Collectively.Services.Notifications.Queries
{
    public class GetUserNotificationSettings : IQuery
    {
        public string UserId { get; set; }
    }
}