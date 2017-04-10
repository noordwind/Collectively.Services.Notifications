using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Messages.Commands.Notifications;
using Collectively.Services.Notifications.Domain;

namespace Collectively.Services.Notifications.Services
{
    public interface IUserNotificationSettingsService
    {
        Task<IEnumerable<User>> BrowseSettingsAsync(IEnumerable<string> userIds);
        Task<Maybe<User>> GetSettingsAsync(string userId);
        Task UpdateSettingsAsync(UpdateUserNotificationSettings newSettings);
    }
}