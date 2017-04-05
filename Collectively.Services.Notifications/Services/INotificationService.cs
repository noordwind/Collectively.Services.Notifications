using System;
using System.Threading.Tasks;

namespace Collectively.Services.Notifications.Services
{
    public interface INotificationService
    {
        Task NotifyRemarkCreatedAsync(Guid remarkId);
        Task NotifyRemarkCanceledAsync(Guid remarkId);
        Task NotifyRemarkDeletedAsync(Guid remarkId);
        Task NotifyRemarkProcessedAsync(Guid remarkId);
        Task NotifyRemarkRenewedAsync(Guid remarkId);
        Task NotifyRemarkResolvedAsync(Guid remarkId);
        Task NotifyPhotosAddedAsync(Guid remarkId);
        Task NotifyCommentAddedAsync(Guid remarkId);
    }

    public class NotificationService : INotificationService
    {
        public Task NotifyRemarkCreatedAsync(Guid remarkId)
        {
            throw new NotImplementedException();
        }

        public Task NotifyRemarkCanceledAsync(Guid remarkId)
        {
            throw new NotImplementedException();
        }

        public Task NotifyRemarkDeletedAsync(Guid remarkId)
        {
            throw new NotImplementedException();
        }

        public Task NotifyRemarkProcessedAsync(Guid remarkId)
        {
            throw new NotImplementedException();
        }

        public Task NotifyRemarkRenewedAsync(Guid remarkId)
        {
            throw new NotImplementedException();
        }

        public Task NotifyRemarkResolvedAsync(Guid remarkId)
        {
            throw new NotImplementedException();
        }

        public Task NotifyPhotosAddedAsync(Guid remarkId)
        {
            throw new NotImplementedException();
        }

        public Task NotifyCommentAddedAsync(Guid remarkId)
        {
            throw new NotImplementedException();
        }
    }
}