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

        public async Task<IEnumerable<User>> BrowseByIdsAsync(IEnumerable<string> userIds)
            => await _database.UserNotificationSettings().BrowseByIdsAsync(userIds);

        public async Task<Maybe<User>> GetByIdAsync(string userId)
            => await _database.UserNotificationSettings().GetByIdAsync(userId);

        public async Task AddAsync(User settings)
            => await _database.UserNotificationSettings().AddAsync(settings);

        public async Task EditAsync(User settings)
            => await _database.UserNotificationSettings().EditAsync(settings);

    }
}