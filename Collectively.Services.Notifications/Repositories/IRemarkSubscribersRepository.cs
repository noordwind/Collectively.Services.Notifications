using System;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Domain;

namespace Collectively.Services.Notifications.Repositories
{
    public interface IRemarkSubscribersRepository
    {
        Task<Maybe<RemarkSubscribers>> GetByIdAsync(Guid remarkId);
        Task AddAsync(RemarkSubscribers subscribers);
        Task EditAsync(RemarkSubscribers subscribers);
    }
}