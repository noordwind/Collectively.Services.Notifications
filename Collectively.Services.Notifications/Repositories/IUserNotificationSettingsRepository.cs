using System.Collections.Generic;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Domain;

namespace Collectively.Services.Notifications.Repositories
{
    public interface IUserNotificationSettingsRepository
    {
        Task<IEnumerable<User>> BrowseByIdsAsync(IEnumerable<string> userIds);
        Task<Maybe<User>> GetByIdAsync(string userId);
        Task AddOrUpdateAsync(User settings);
    }
}