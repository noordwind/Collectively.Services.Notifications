using System.Collections.Generic;
using System.Threading.Tasks;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Models;

namespace Collectively.Services.Notifications.Services
{
    public interface IEmailMessageService
    {
        Task PublishRemarkCreatedEmailAsync(IEnumerable<User> users, Remark remark);
        Task PublishRemarkStateChangedEmailAsync(IEnumerable<User> users, Remark remark);
        Task PublishPhotosAddedToRemarkEmailAsync(IEnumerable<User> users, Remark remark, string author);
        Task PublishCommentAddedToRemarkEmailAsync(IEnumerable<User> users, Remark value, string author, string comment);
    }
}