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
        public static IMongoCollection<User> UserNotificationSettings(this IMongoDatabase database)
            => database.GetCollection<User>();

        public static async Task<IEnumerable<User>> BrowseByIdsAsync(
            this IMongoCollection<User> collection, IEnumerable<string> userIds)
        {
            if (userIds == null)
                return Enumerable.Empty<User>();

            var enumerable = userIds as string[] ?? userIds.ToArray();
            if (enumerable.Any() == false)
                return Enumerable.Empty<User>();

            return await collection.AsQueryable()
                .Where(x => enumerable.Contains(x.UserId))
                .ToListAsync();
        }

        public static async Task<User> GetByIdAsync(
            this IMongoCollection<User> collection, string userId)
        {
            if (userId.Empty())
                return null;

            return await collection.AsQueryable().FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public static async Task AddOrUpdateAsync(
            this IMongoCollection<User> collection, User settings)
            => await collection.ReplaceOneAsync(x => x.UserId == settings.UserId, settings, new UpdateOptions
            {
                IsUpsert = true
            });
    }
}