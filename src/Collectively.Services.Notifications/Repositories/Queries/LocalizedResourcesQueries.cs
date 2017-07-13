using System;
using System.Threading.Tasks;
using Collectively.Common.Mongo;
using Collectively.Common.Extensions;
using Collectively.Services.Notifications.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Collectively.Services.Notifications.Repositories.Queries
{
    public static class LocalizedResourcesQueries
    {
        public static IMongoCollection<LocalizedResource> LocalizedResources(this IMongoDatabase database)
            => database.GetCollection<LocalizedResource>();

        public static async Task<LocalizedResource> GetAsync(this IMongoCollection<LocalizedResource> resources,
            Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await resources.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<LocalizedResource> GetAsync(this IMongoCollection<LocalizedResource> resources,
            string name, string culture)
        {
            if (name.Empty() || culture.Empty())
                return null;

            return await resources.AsQueryable().FirstOrDefaultAsync(x => x.Name == name && x.Culture.StartsWith(culture));
        }
    }
}