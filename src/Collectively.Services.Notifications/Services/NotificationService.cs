using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Models;
using Collectively.Services.Notifications.ServiceClients;
using NLog;

namespace Collectively.Services.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IRemarkServiceClient _remarkServiceClient;
        private readonly IRemarkSubscribersService _subscribersService;
        private readonly IUserNotificationSettingsService _userNotificationSettingsService;
        private readonly IEmailMessageService _emailService;

        public NotificationService(IRemarkServiceClient remarkServiceClient,
            IRemarkSubscribersService subscribersService,
            IUserNotificationSettingsService userNotificationSettingsService,
            IEmailMessageService emailService)
        {
            _remarkServiceClient = remarkServiceClient;
            _subscribersService = subscribersService;
            _userNotificationSettingsService = userNotificationSettingsService;
            _emailService = emailService;
        }

        public async Task NotifyRemarkCreatedAsync(Guid remarkId)
        {
            Logger.Debug($"Send RemarkCreatedEmail, remarkId: {remarkId}");
            var remark = await GetRemarkAsync(remarkId);
            var subsribers = await GetSubscribersAsync(remarkId);

            if (subsribers.HasNoValue)
                return;

            var users = await GetUsersAsync(subsribers.Value);
            await _emailService.PublishRemarkCreatedEmailAsync(users, remark.Value);
        }

        public async Task NotifyRemarkCanceledAsync(Guid remarkId)
        {
            Logger.Debug($"Send RemarkCanceledEmail, remarkId: {remarkId}");
            var remark = await GetRemarkAsync(remarkId);
            var subsribers = await GetSubscribersAsync(remarkId);

            if (subsribers.HasNoValue)
                return;

            var users = await GetUsersAsync(subsribers.Value);
            await _emailService.PublishRemarkCanceledEmailAsync(users, remark.Value);
        }

        public async Task NotifyRemarkDeletedAsync(Guid remarkId)
        {
            Logger.Debug($"Send RemarkDeletedEmail, remarkId: {remarkId}");
            var remark = await GetRemarkAsync(remarkId);
            var subsribers = await GetSubscribersAsync(remarkId);

            if (subsribers.HasNoValue)
                return;

            var users = await GetUsersAsync(subsribers.Value);
            await _emailService.PublishRemarkDeletedEmailAsync(users, remark.Value);
        }

        public async Task NotifyRemarkProcessedAsync(Guid remarkId)
        {
            Logger.Debug($"Send RemarkProcessedEmail, remarkId: {remarkId}");
            var remark = await GetRemarkAsync(remarkId);
            var subsribers = await GetSubscribersAsync(remarkId);

            if (subsribers.HasNoValue)
                return;

            var users = await GetUsersAsync(subsribers.Value);
            await _emailService.PublishRemarkProcessedEmailAsync(users, remark.Value);
        }

        public async Task NotifyRemarkRenewedAsync(Guid remarkId)
        {
            Logger.Debug($"Send RemarkRenewedEmail, remarkId: {remarkId}");
            var remark = await GetRemarkAsync(remarkId);
            var subsribers = await GetSubscribersAsync(remarkId);

            if (subsribers.HasNoValue)
                return;

            var users = await GetUsersAsync(subsribers.Value);
            await _emailService.PublishRemarkRenewedEmailAsync(users, remark.Value);
        }

        public async Task NotifyRemarkResolvedAsync(Guid remarkId)
        {
            Logger.Debug($"Send RemarkResolvedEmail, remarkId: {remarkId}");
            var remark = await GetRemarkAsync(remarkId);
            var subsribers = await GetSubscribersAsync(remarkId);

            if (subsribers.HasNoValue)
                return;

            var users = await GetUsersAsync(subsribers.Value);
            await _emailService.PublishRemarkResolvedEmailAsync(users, remark.Value);
        }


        public async Task NotifyPhotoAddedAsync(Guid remarkId)
        {
            Logger.Debug($"Send RemarkStateChangedEmail, remarkId: {remarkId}");
            var remark = await GetRemarkAsync(remarkId);
            var subsribers = await GetSubscribersAsync(remarkId);

            if (subsribers.HasNoValue)
                return;

            var users = await GetUsersAsync(subsribers.Value);
            await _emailService.PublishPhotosAddedToRemarkEmailAsync(users, remark.Value);
        }

        public async Task NotifyCommentAddedAsync(Guid remarkId, string author, string comment, DateTime date)
        {
            Logger.Debug($"Send RemarkStateChangedEmail, remarkId: {remarkId}");
            var remark = await GetRemarkAsync(remarkId);
            var subsribers = await GetSubscribersAsync(remarkId);

            if (subsribers.HasNoValue)
                return;

            var users = await GetUsersAsync(subsribers.Value);
            await _emailService.PublishCommentAddedToRemarkEmailAsync(users, remark.Value, author, comment, date);
        }

        protected async Task<Maybe<Remark>> GetRemarkAsync(Guid remarkId) 
            => await _remarkServiceClient.GetAsync(remarkId);

        protected async Task<Maybe<RemarkSubscribers>> GetSubscribersAsync(Guid remarkId)
            => await _subscribersService.GetSubscribersAsync(remarkId);

        protected async Task<IEnumerable<UserNotificationSettings>> GetUsersAsync(RemarkSubscribers subscribers)
            => await _userNotificationSettingsService
                .BrowseSettingsAsync(subscribers.Users);
    }
}