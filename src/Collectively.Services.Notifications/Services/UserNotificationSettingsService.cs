using System.Collections.Generic;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Messages.Commands.Notifications;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Repositories;

namespace Collectively.Services.Notifications.Services
{
    public class UserNotificationSettingsService : IUserNotificationSettingsService
    {
        private readonly IUserNotificationSettingsRepository _repository;

        public UserNotificationSettingsService(IUserNotificationSettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserNotificationSettings>> BrowseSettingsAsync(IEnumerable<string> userIds)
            => await _repository.BrowseByIdsAsync(userIds);

        public async Task<Maybe<UserNotificationSettings>> GetSettingsAsync(string userId)
            => await _repository.GetByIdAsync(userId);

        public async Task UpdateSettingsAsync(UserNotificationSettings newSettings)
        {
            var currentSettings = await _repository.GetByIdAsync(newSettings.UserId);
            if (currentSettings.HasValue)
            {
                currentSettings.Value.Update(newSettings);
                newSettings = currentSettings.Value;
            }

            await _repository.AddOrUpdateAsync(newSettings);
        }
    }
}