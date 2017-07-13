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
        Task NotifyPhotoAddedAsync(Guid remarkId);
        Task NotifyCommentAddedAsync(Guid remarkId, string author, string comment, DateTime date);
    }
}