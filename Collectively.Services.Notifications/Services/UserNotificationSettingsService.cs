using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Collectively.Common.Types;
using Collectively.Messages.Commands.Notifications;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Repositories;

namespace Collectively.Services.Notifications.Services
{
    public class UserNotificationSettingsService : IUserNotificationSettingsService
    {
        private readonly IUserNotificationSettingsRepository _repository;
        private readonly IMapper _mapper;

        public UserNotificationSettingsService(IUserNotificationSettingsRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> BrowseSettingsAsync(IEnumerable<string> userIds)
            => await _repository.BrowseByIdsAsync(userIds);

        public async Task<Maybe<User>> GetSettingsAsync(string userId)
            => await _repository.GetByIdAsync(userId);

        public async Task UpdateSettingsAsync(UpdateUserNotificationSettings newSettings)
        {
            var settings = _mapper.Map<User>(newSettings);
            var currentSettings = await _repository.GetByIdAsync(newSettings.UserId);
            if (currentSettings.HasValue)
            {
                currentSettings.Value.Update(settings);
                settings = currentSettings.Value;
            }

            await _repository.AddOrUpdateAsync(settings);
        }
    }
}