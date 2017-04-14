using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collectively.Messages.Commands;
using Collectively.Messages.Commands.Mailing;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Models;
using Collectively.Services.Notifications.Repositories;
using Collectively.Services.Notifications.Settings;
using RawRabbit;

namespace Collectively.Services.Notifications.Services
{
    public class EmailMessageService : IEmailMessageService
    {
        private readonly IBusClient _busClient;
        private readonly ILocalizedResourceRepository _localizedResourceRepository;
        private readonly GeneralSettings _settings;

        public EmailMessageService(IBusClient busClient,
            ILocalizedResourceRepository localizedResourceRepository,
            GeneralSettings settings)
        {
            _busClient = busClient;
            _localizedResourceRepository = localizedResourceRepository;
            _settings = settings;
        }

        public async Task PublishRemarkCreatedEmailAsync(IEnumerable<UserNotificationSettings> users, Remark remark)
        {
            if (users == null)
                return;

            var recipients = users
                .Where(u => u.EmailSettings.Enabled && u.EmailSettings.RemarkCreated)
                .ToList();

            foreach (var user in recipients)
            {
                var message = new SendRemarkCreatedEmailMessage
                {
                    Request = Request.New<SendRemarkCreatedEmailMessage>(),
                    Address = remark.Location.Address,
                    Category = await GetTranslatedCategoryAsync(user.Culture, remark.Category.Name),
                    Date = remark.CreatedAt,
                    Email = user.Email,
                    RemarkId = remark.Id,
                    Username = remark.Author.Name,
                    Culture = user.Culture,
                    RemarkUrl = $"{_settings.RemarksPath}{remark.Id}"
                };
                await _busClient.PublishAsync(message);
            }
        }

        public async Task PublishRemarkCanceledEmailAsync(IEnumerable<UserNotificationSettings> users, Remark remark)
        {
            if (users == null)
                return;

            var recipients = users
                .Where(u => u.EmailSettings.Enabled && u.EmailSettings.RemarkCanceled)
                .ToList();

            await PublishRemarkStateChangedEmailAsync(recipients, remark);
        }

        public async Task PublishRemarkDeletedEmailAsync(IEnumerable<UserNotificationSettings> users, Remark remark)
        {
            if (users == null)
                return;

            var recipients = users
                .Where(u => u.EmailSettings.Enabled && u.EmailSettings.RemarkDeleted)
                .ToList();

            await PublishRemarkStateChangedEmailAsync(recipients, remark);
        }

        public async Task PublishRemarkProcessedEmailAsync(IEnumerable<UserNotificationSettings> users, Remark remark)
        {
            if (users == null)
                return;

            var recipients = users
                .Where(u => u.EmailSettings.Enabled && u.EmailSettings.RemarkProcessed)
                .ToList();

            await PublishRemarkStateChangedEmailAsync(recipients, remark);
        }

        public async Task PublishRemarkRenewedEmailAsync(IEnumerable<UserNotificationSettings> users, Remark remark)
        {
            if (users == null)
                return;

            var recipients = users
                .Where(u => u.EmailSettings.Enabled && u.EmailSettings.RemarkRenewed)
                .ToList();

            await PublishRemarkStateChangedEmailAsync(recipients, remark);
        }

        public async Task PublishRemarkResolvedEmailAsync(IEnumerable<UserNotificationSettings> users, Remark remark)
        {
            if (users == null)
                return;

            var recipients = users
                .Where(u => u.EmailSettings.Enabled && u.EmailSettings.RemarkResolved)
                .ToList();

            await PublishRemarkStateChangedEmailAsync(recipients, remark);
        }

        public async Task PublishPhotosAddedToRemarkEmailAsync(IEnumerable<UserNotificationSettings> users, Remark remark, string author)
        {
            if (users == null)
                return;

            var recipients = users
                .Where(u => u.EmailSettings.Enabled && u.EmailSettings.PhotosToRemarkAdded)
                .ToList();

            foreach (var user in recipients)
            {
                var message = new SendPhotosAddedToRemarkEmailMessage
                {
                    Request = Request.New<SendPhotosAddedToRemarkEmailMessage>(),
                    Address = remark.Location.Address,
                    Category = await GetTranslatedCategoryAsync(user.Culture, remark.Category.Name),
                    Email = user.Email,
                    RemarkId = remark.Id,
                    Username = author,
                    Culture = user.Culture,
                    RemarkUrl = $"{_settings.RemarksPath}{remark.Id}"
                };
                await _busClient.PublishAsync(message);
            }
        }

        public async Task PublishCommentAddedToRemarkEmailAsync(IEnumerable<UserNotificationSettings> users, Remark remark, string author, string comment)
        {
            if (users == null)
                return;

            var recipients = users
                .Where(u => u.EmailSettings.Enabled && u.EmailSettings.CommentAdded)
                .ToList();

            foreach (var user in recipients)
            {
                var message = new SendCommentAddedToRemarkEmailMessage
                {
                    Request = Request.New<SendCommentAddedToRemarkEmailMessage>(),
                    Address = remark.Location.Address,
                    Category = await GetTranslatedCategoryAsync(user.Culture, remark.Category.Name),
                    Email = user.Email,
                    RemarkId = remark.Id,
                    Username = author,
                    Comment = comment,
                    Culture = user.Culture,
                    RemarkUrl = $"{_settings.RemarksPath}{remark.Id}"
                };
                await _busClient.PublishAsync(message);
            }
        }

        protected async Task PublishRemarkStateChangedEmailAsync(IEnumerable<UserNotificationSettings> users, Remark remark)
        {
            if (users == null)
                return;

            if (remark == null)
                return;

            foreach (var user in users)
            {
                var message = new SendRemarkStateChangedEmailMessage
                {
                    Request = Request.New<SendRemarkStateChangedEmailMessage>(),
                    Address = remark.Location.Address,
                    Category = await GetTranslatedCategoryAsync(user.Culture, remark.Category.Name),
                    Email = user.Email,
                    RemarkId = remark.Id,
                    Date = remark.State.CreatedAt,
                    Username = remark.State.User.Name,
                    State = await GetTranslatedStateAsync(user.Culture, remark.State.State),
                    Culture = user.Culture,
                    RemarkUrl = $"{_settings.RemarksPath}{remark.Id}"
                };
                await _busClient.PublishAsync(message);
            }
        }

        protected async Task<string> GetTranslatedStateAsync(string culture, string state)
        {
            var resource = await _localizedResourceRepository.GetAsync($"state:{state}", culture);

            return resource.HasValue ? resource.Value.Text : state;
        }

        protected async Task<string> GetTranslatedCategoryAsync(string culture, string category)
        {
            var resource = await _localizedResourceRepository.GetAsync($"category:{category}", culture);

            return resource.HasValue ? resource.Value.Text : category;
        }
    }
}