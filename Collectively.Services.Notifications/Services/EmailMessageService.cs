using System.Collections.Generic;
using System.Threading.Tasks;
using Collectively.Messages.Commands;
using Collectively.Messages.Commands.Mailing;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Models;
using RawRabbit;

namespace Collectively.Services.Notifications.Services
{
    public class EmailMessageService : IEmailMessageService
    {
        private readonly IBusClient _busClient;

        public EmailMessageService(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task PublishRemarkCreatedEmailAsync(IEnumerable<User> users, Remark remark)
        {
            if (users == null)
                return;

            foreach (var user in users)
            {
                var message = new SendRemarkCreatedEmailMessage
                {
                    Request = Request.New<SendRemarkCreatedEmailMessage>(),
                    Address = remark.Location.Address,
                    Category = remark.Category.Name,
                    Date = remark.CreatedAt,
                    Email = user.Email,
                    RemarkId = remark.Id,
                    Username = remark.Author.Name,
                    Culture = user.Culture
                };
                await _busClient.PublishAsync(message);
            }
        }

        public async Task PublishRemarkStateChangedEmailAsync(IEnumerable<User> users, Remark remark)
        {
            if (users == null)
                return;

            foreach (var user in users)
            {
                var message = new SendRemarkStateChangedEmailMessage
                {
                    Request = Request.New<SendRemarkStateChangedEmailMessage>(),
                    Address = remark.Location.Address,
                    Category = remark.Category.Name,
                    Email = user.Email,
                    RemarkId = remark.Id,
                    Date = remark.State.CreatedAt,
                    Username = remark.State.User.Name,
                    State = remark.State.State,
                    Culture = user.Culture
                };
                await _busClient.PublishAsync(message);
            }
        }

        public async Task PublishPhotosAddedToRemarkEmailAsync(IEnumerable<User> users, Remark remark, string author)
        {
            if (users == null)
                return;

            foreach (var user in users)
            {
                var message = new SendPhotosAddedToRemarkEmailMessage
                {
                    Request = Request.New<SendPhotosAddedToRemarkEmailMessage>(),
                    Address = remark.Location.Address,
                    Category = remark.Category.Name,
                    Email = user.Email,
                    RemarkId = remark.Id,
                    Username = author,
                    Culture = user.Culture
                };
                await _busClient.PublishAsync(message);
            }
        }

        public async Task PublishCommentAddedToRemarkEmailAsync(IEnumerable<User> users, Remark remark, string author, string comment)
        {
            if (users == null)
                return;

            foreach (var user in users)
            {
                var message = new SendCommentAddedToRemarkEmailMessage
                {
                    Request = Request.New<SendCommentAddedToRemarkEmailMessage>(),
                    Address = remark.Location.Address,
                    Category = remark.Category.Name,
                    Email = user.Email,
                    RemarkId = remark.Id,
                    Username = author,
                    Comment = comment,
                    Culture = user.Culture
                };
                await _busClient.PublishAsync(message);
            }
        }
    }
}