using System;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Models;
using Collectively.Services.Notifications.ServiceClients;
using NLog;
using RawRabbit;

namespace Collectively.Services.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IRemarkServiceClient _remarkServiceClient;
        private readonly IUserServiceClient _userServiceClient;
        private readonly IEmailMessageService _emailService;

        public NotificationService(IRemarkServiceClient remarkServiceClient,
            IUserServiceClient userServiceClient,
            IEmailMessageService emailService)
        {
            _remarkServiceClient = remarkServiceClient;
            _userServiceClient = userServiceClient;
            _emailService = emailService;
        }

        public async Task NotifyRemarkCreatedAsync(Guid remarkId)
        {
            var remark = await GetRemarkAsync(remarkId);

            Logger.Debug($"Send RemarkCreatedEmail, remarkId: {remarkId}");
            throw new NotImplementedException();
        }

        public async Task NotifyRemarkCanceledAsync(Guid remarkId)
            => await NotifyRemarkStateChangedAsync(remarkId);

        public async Task NotifyRemarkDeletedAsync(Guid remarkId)
            => await NotifyRemarkStateChangedAsync(remarkId);

        public async Task NotifyRemarkProcessedAsync(Guid remarkId)
            => await NotifyRemarkStateChangedAsync(remarkId);

        public async Task NotifyRemarkRenewedAsync(Guid remarkId)
            => await NotifyRemarkStateChangedAsync(remarkId);

        public async Task NotifyRemarkResolvedAsync(Guid remarkId)
            => await NotifyRemarkStateChangedAsync(remarkId);


        public async Task NotifyPhotosAddedAsync(Guid remarkId)
        {
            throw new NotImplementedException();
        }

        public async Task NotifyCommentAddedAsync(Guid remarkId)
        {
            throw new NotImplementedException();
        }

        protected async Task NotifyRemarkStateChangedAsync(Guid remarkId)
        {
            var remark = await GetRemarkAsync(remarkId);

            Logger.Debug($"Send RemarkStateChangedEmail, remarkId: {remarkId}");
            throw new NotImplementedException();
        }

        protected async Task<Maybe<Remark>> GetRemarkAsync(Guid remarkId) 
            => await _remarkServiceClient.GetAsync(remarkId);

        protected async Task<Maybe<User>> GetUserAsync(string userId)
            => await _userServiceClient.GetAsync(userId);
    }
}