using System.Threading.Tasks;
using Collectively.Common.Services;
using Collectively.Messages.Events;
using Collectively.Messages.Events.Users;
using Collectively.Services.Notifications.Services;

namespace Collectively.Services.Notifications.Handlers
{
    public class UsernameChangedHandler : IEventHandler<UsernameChanged>
    {
        private readonly IHandler _handler;
        private readonly IUserNotificationSettingsService _settingsService;

        public UsernameChangedHandler(IHandler handler,
            IUserNotificationSettingsService settingsService)
        {
            _handler = handler;
            _settingsService = settingsService;
        }

        public async Task HandleAsync(UsernameChanged @event)
        {
            await _handler
                .Run(async () =>
                {
                    var settings = await _settingsService.GetSettingsAsync(@event.UserId);
                    settings.Value.Username = @event.NewName;
                    await _settingsService.UpdateSettingsAsync(settings.Value);
                })
                .OnError((ex, logger) => logger.Error(ex, $"Error while updating username, userId: {@event.UserId}"))
                .ExecuteAsync();
        }
    }
}