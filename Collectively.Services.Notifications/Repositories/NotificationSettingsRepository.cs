using System;
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

        public async Task<Maybe<UserNotificationSettings>> GetByIdAsync(Guid userId)
            => await _database.UserNotificationSettings().GetByIdAsync(userId);

        public async Task AddAsync(UserNotificationSettings settings)
            => await _database.UserNotificationSettings().AddAsync(settings);

        public async Task EditAsync(UserNotificationSettings settings)
            => await _database.UserNotificationSettings().EditAsync(settings);

    }
}