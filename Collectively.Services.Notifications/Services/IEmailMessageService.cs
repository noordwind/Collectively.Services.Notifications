using System.Collections.Generic;
using System.Threading.Tasks;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Models;

namespace Collectively.Services.Notifications.Services
{
    public interface IEmailMessageService
    {
        Task PublishRemarkCreatedEmailAsync(IEnumerable<UserNotificationSettings> users, Remark remark);
        Task PublishRemarkStateChangedEmailAsync(IEnumerable<UserNotificationSettings> users, Remark remark);
        Task PublishPhotosAddedToRemarkEmailAsync(IEnumerable<UserNotificationSettings> users, Remark remark, string author);
        Task PublishCommentAddedToRemarkEmailAsync(IEnumerable<UserNotificationSettings> users, Remark value, string author, string comment);
    }
}