using System;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Repositories.Queries;
using MongoDB.Driver;

namespace Collectively.Services.Notifications.Repositories
{
    public class RemarkSubscribersRepository : IRemarkSubscribersRepository
    {
        private readonly IMongoDatabase _database;

        public RemarkSubscribersRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<RemarkSubscribers>> GetByIdAsync(Guid remarkId)
            => await _database.RemarkSubscribers().GetByIdAsync(remarkId);

        public async Task AddAsync(RemarkSubscribers subscribers)
            => await _database.RemarkSubscribers().AddAsync(subscribers);

        public async Task EditAsync(RemarkSubscribers subscribers)
            => await _database.RemarkSubscribers().EditAsync(subscribers);
    }
}