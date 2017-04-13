using System.Threading.Tasks;
using Collectively.Common.Domain;
using Collectively.Common.Services;
using Collectively.Messages.Events;
using Collectively.Messages.Events.Users;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.ServiceClients;
using Collectively.Services.Notifications.Services;

namespace Collectively.Services.Notifications.Handlers
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private readonly IHandler _handler;
        private readonly IUserNotificationSettingsService _settingsService;
        private readonly IUserServiceClient _userServiceClient;

        public SignedUpHandler(IHandler handler,
            IUserNotificationSettingsService settingsService,
            IUserServiceClient userServiceClient)
        {
            _handler = handler;
            _settingsService = settingsService;
            _userServiceClient = userServiceClient;
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

                    var settings = UserNotificationSettings.Create(@event.UserId, 
                        user.Value.Email, user.Value.Name, "en-gb");
                    await _settingsService.UpdateSettingsAsync(settings);
                })
                .OnError((ex, logger) => logger.Error(ex, $"Error while handling user SignedUp event, userId: {@event.UserId}"))
                .ExecuteAsync();
        }
    }
}