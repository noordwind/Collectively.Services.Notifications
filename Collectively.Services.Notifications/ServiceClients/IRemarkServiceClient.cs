using System;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Models;

namespace Collectively.Services.Notifications.ServiceClients
{
    public interface IRemarkServiceClient
    {
        Task<Maybe<Remark>> GetAsync(Guid id);
    }
}