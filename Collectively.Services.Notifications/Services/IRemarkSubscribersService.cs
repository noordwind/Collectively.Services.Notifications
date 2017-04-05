using System;
using System.Threading.Tasks;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Repositories;

namespace Collectively.Services.Notifications.Services
{
    public interface IRemarkSubscribersService
    {
        Task AddSubscriberAsync(Guid remarkId, string userId);
    }

    public class RemarkSubscribersService : IRemarkSubscribersService
    {
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
                subsribers = new RemarkSubscribers(remarkId);
            }
            subsribers.Value.AddSubscriber(userId);
            await _repository.AddOrUpdateAsync(subsribers.Value);
        }
    }
}