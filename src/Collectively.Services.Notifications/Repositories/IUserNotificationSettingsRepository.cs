using System.Collections.Generic;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Domain;

namespace Collectively.Services.Notifications.Repositories
{
    public interface IUserNotificationSettingsRepository
    {
        Task<IEnumerable<UserNotificationSettings>> BrowseByIdsAsync(IEnumerable<string> userIds);
        Task<Maybe<UserNotificationSettings>> GetByIdAsync(string userId);
        Task AddOrUpdateAsync(UserNotificationSettings settings);
    }
}