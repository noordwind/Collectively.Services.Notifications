using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collectively.Common.Extensions;
using Collectively.Common.Mongo;
using Collectively.Services.Notifications.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Collectively.Services.Notifications.Repositories.Queries
{
    public static class UserNotificationSettingsQueries
    {
        public static IMongoCollection<UserNotificationSettings> UserNotificationSettings(this IMongoDatabase database)
            => database.GetCollection<UserNotificationSettings>();

        public static async Task<IEnumerable<UserNotificationSettings>> BrowseByIdsAsync(
            this IMongoCollection<UserNotificationSettings> collection, IEnumerable<string> userIds)
        {
            if (userIds == null)
                return Enumerable.Empty<UserNotificationSettings>();

            var enumerable = userIds as string[] ?? userIds.ToArray();
            if (enumerable.Any() == false)
                return Enumerable.Empty<UserNotificationSettings>();

            return await collection.AsQueryable()
                .Where(x => enumerable.Contains(x.UserId))
                .ToListAsync();
        }

        public static async Task<UserNotificationSettings> GetByIdAsync(
            this IMongoCollection<UserNotificationSettings> collection, string userId)
        {
            if (userId.Empty())
                return null;

            return await collection.AsQueryable().FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public static async Task AddOrUpdateAsync(
            this IMongoCollection<UserNotificationSettings> collection, UserNotificationSettings settings)
            => await collection.ReplaceOneAsync(x => x.UserId == settings.UserId, settings, new UpdateOptions
            {
                IsUpsert = true
            });
    }
}