using System.Collections.Generic;
using System.Threading.Tasks;
using Collectively.Services.Notifications.Models;

namespace Collectively.Services.Notifications.Services
{
    public interface IEmailMessageService
    {
        Task PublishRemarkCreatedEmailAsync(IEnumerable<User> users, Remark remark);
        Task PublishRemarkStateChangedEmailAsync(IEnumerable<User> users, Remark remark);
        Task PublishPhotosAddedToRemarkEmailAsync(IEnumerable<User> users, Remark remark);
        Task PublishSendCommentAddedToRemarkEmailAsync(IEnumerable<User> users, Remark remark);
    }

    public class EmailMessageService : IEmailMessageService
    {
        public Task PublishRemarkCreatedEmailAsync(IEnumerable<User> users, Remark remark)
        {
            throw new System.NotImplementedException();
        }

        public Task PublishRemarkStateChangedEmailAsync(IEnumerable<User> users, Remark remark)
        {
            throw new System.NotImplementedException();
        }

        public Task PublishPhotosAddedToRemarkEmailAsync(IEnumerable<User> users, Remark remark)
        {
            throw new System.NotImplementedException();
        }

        public Task PublishSendCommentAddedToRemarkEmailAsync(IEnumerable<User> users, Remark remark)
        {
            throw new System.NotImplementedException();
        }
    }
}