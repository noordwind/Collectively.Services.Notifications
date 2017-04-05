using System;
using System.Threading.Tasks;
using Collectively.Common.Mongo;
using Collectively.Services.Notifications.Domain;
using MongoDB.Driver;

namespace Collectively.Services.Notifications.Repositories.Queries
{
    public static class RemarkSubscribersQueries
    {
        public static IMongoCollection<RemarkSubscribers> RemarkSubscribers(this IMongoDatabase database)
            => database.GetCollection<RemarkSubscribers>();

        public static async Task<RemarkSubscribers> GetByIdAsync
            (this IMongoCollection<RemarkSubscribers> collection, Guid remarkId)
            => await collection.FirstOrDefaultAsync(x => x.RemarkId == remarkId);

        public static async Task AddAsync
            (this IMongoCollection<RemarkSubscribers> collection, RemarkSubscribers subscribers)
            => await collection.InsertOneAsync(subscribers);

        public static async Task EditAsync
            (this IMongoCollection<RemarkSubscribers> collection, RemarkSubscribers subscribers)
            => await collection.ReplaceOneAsync(x => x.RemarkId == subscribers.RemarkId, subscribers);
    }
}