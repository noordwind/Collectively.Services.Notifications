using System.Threading.Tasks;
using AutoMapper;
using Collectively.Common.Types;
using Collectively.Messages.Commands.Notifications;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Repositories;

namespace Collectively.Services.Notifications.Services
{
    public interface IUserNotificationSettingsService
    {
        Task<Maybe<UserNotificationSettings>> GetSettingsAsync(string userId);
        Task UpdateSettingsAsync(UpdateUserNotificationSettings newSettings);
    }

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

        public async Task<Maybe<UserNotificationSettings>> GetSettingsAsync(string userId)
            => await _repository.GetByIdAsync(userId);

        public async Task UpdateSettingsAsync(UpdateUserNotificationSettings newSettings)
        {
            var settings = await _repository.GetByIdAsync(newSettings.UserId);
            if (settings.HasNoValue)
            {
                settings = _mapper.Map<UserNotificationSettings>(newSettings);
                await _repository.AddAsync(settings.Value);
            }
            else
            {
                settings.Value.EmailSettings = _mapper
                    .Map<NotificationSettings>(newSettings.EmailSettings);
                settings.Value.PushSettings = _mapper
                    .Map<NotificationSettings>(newSettings.PushSettings);
                await _repository.EditAsync(settings.Value);
            }
        }
    }
}