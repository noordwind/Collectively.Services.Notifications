using System.Collections.Generic;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Repositories.Queries;
using MongoDB.Driver;

namespace Collectively.Services.Notifications.Repositories
{
    public class UserNotificationSettingsRepository : IUserNotificationSettingsRepository
    {
        private readonly IMongoDatabase _database;

        public UserNotificationSettingsRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<UserNotificationSettings>> BrowseByIdsAsync(IEnumerable<string> userIds)
            => await _database.UserNotificationSettings().BrowseByIdsAsync(userIds);

        public async Task<Maybe<UserNotificationSettings>> GetByIdAsync(string userId)
            => await _database.UserNotificationSettings().GetByIdAsync(userId);

        public async Task AddOrUpdateAsync(UserNotificationSettings settings)
            => await _database.UserNotificationSettings().AddOrUpdateAsync(settings);

    }
}