using System.Linq;
using System.Threading.Tasks;
using Collectively.Common.Domain;
using Collectively.Common.Services;
using Collectively.Messages.Events;
using Collectively.Messages.Events.Users;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.ServiceClients;
using Collectively.Services.Notifications.Services;
using Collectively.Services.Notifications.Settings;

namespace Collectively.Services.Notifications.Handlers
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private readonly IHandler _handler;
        private readonly IUserNotificationSettingsService _settingsService;
        private readonly IUserServiceClient _userServiceClient;
        private readonly GeneralSettings _generalSettings;

        public SignedUpHandler(IHandler handler,
            IUserNotificationSettingsService settingsService,
            IUserServiceClient userServiceClient,
            GeneralSettings generalSettings)
        {
            _handler = handler;
            _settingsService = settingsService;
            _userServiceClient = userServiceClient;
            _generalSettings = generalSettings;
        }

        public async Task HandleAsync(SignedUp @event)
        {
            await _handler
                .Run(async () =>
                {
                    var user = await _userServiceClient.GetAsync(@event.UserId);
                    if (user.HasNoValue)
                        throw new ServiceException(OperationCodes.ResourceNotFound,
                            $"User with id: {@event.UserId} cannot be found");
                    var culture = GetCulture(user.Value.Culture);
                    var settings = UserNotificationSettings.Create(@event.UserId, 
                        user.Value.Email, user.Value.Name, culture);
                    await _settingsService.UpdateSettingsAsync(settings);
                })
                .OnError((ex, logger) => logger.Error(ex, $"Error while handling user SignedUp event, userId: {@event.UserId}"))
                .ExecuteAsync();
        }

        private string GetCulture(string culture)
        {
            if (culture == null)
                return _generalSettings.DefaultCulture;

            var supportedCulture = _generalSettings
                .SupportedCultures
                .FirstOrDefault(c => c.Contains(culture));

            return supportedCulture ?? _generalSettings.DefaultCulture;
        }
    }
}