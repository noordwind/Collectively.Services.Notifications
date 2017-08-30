using System;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Repositories;
using Serilog;

namespace Collectively.Services.Notifications.Services
{
    public class RemarkSubscribersService : IRemarkSubscribersService
    {
        private static readonly ILogger Logger = Log.Logger;
        private readonly IRemarkSubscribersRepository _repository;

        public RemarkSubscribersService(IRemarkSubscribersRepository repository)
        {
            _repository = repository;
        }

        public async Task AddSubscriberAsync(Guid remarkId, string userId)
        {
            var subsribers = await _repository.GetByIdAsync(remarkId);
            if (subsribers.HasNoValue)
            {
                Logger.Debug($"RemarkSubsribers not found for remarkId: {remarkId}. Creating new one.");
                subsribers = new RemarkSubscribers(remarkId);
            }
            subsribers.Value.AddSubscriber(userId);
            Logger.Debug($"Add subscriber. remarkId: {remarkId}, userId: {userId}");
            await _repository.AddOrUpdateAsync(subsribers.Value);
        }

        public async Task<Maybe<RemarkSubscribers>> GetSubscribersAsync(Guid remarkId)
            => await _repository.GetByIdAsync(remarkId);

        public async Task RemoveSubscribersAsync(Guid remarkId)
        {
            Logger.Debug($"Remove all subscribers, remarkId: {remarkId}");
            await _repository.RemoveAsync(remarkId);
        }

        public async Task RemoveSubscriberAsync(Guid remarkId, string userId)
        {
            var subscribers = await GetSubscribersAsync(remarkId);
            if (subscribers.HasNoValue)
            {
                Logger.Debug($"RemarkSubsribers not found for remarkId: {remarkId}. Nothing to remove");
                return;
            }

            subscribers.Value.RemoveSubscriber(userId);
            Logger.Debug($"Remove subscriber. remarkId: {remarkId}, userId: {userId}");
            await _repository.AddOrUpdateAsync(subscribers.Value);
        }
    }
}