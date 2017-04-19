using System;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Domain;

namespace Collectively.Services.Notifications.Services
{
    public interface IRemarkSubscribersService
    {
        Task AddSubscriberAsync(Guid remarkId, string userId);
        Task<Maybe<RemarkSubscribers>> GetSubscribersAsync(Guid remarkId);
        Task RemoveSubscribersAsync(Guid remarkId);
        Task RemoveSubscriberAsync(Guid remarkId, string userId);
    }
}