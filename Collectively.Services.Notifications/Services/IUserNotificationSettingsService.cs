using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Domain;

namespace Collectively.Services.Notifications.Services
{
    public interface IUserNotificationSettingsService
    {
        Task<IEnumerable<UserNotificationSettings>> BrowseSettingsAsync(IEnumerable<string> userIds);
        Task<Maybe<UserNotificationSettings>> GetSettingsAsync(string userId);
        Task UpdateSettingsAsync(UserNotificationSettings newSettings);
    }
}