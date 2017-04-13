using System.Threading.Tasks;
using AutoMapper;
using Collectively.Common.Services;
using Collectively.Messages.Commands;
using Collectively.Messages.Commands.Notifications;
using Collectively.Messages.Events.Notifications;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Services;
using RawRabbit;

namespace Collectively.Services.Notifications.Handlers
{
    public class UpdateUserNotificationSettingsHandler : ICommandHandler<UpdateUserNotificationSettings>
    {
        private readonly IHandler _handler;
        private readonly IMapper _mapper;
        private readonly IBusClient _bus;
        private readonly IUserNotificationSettingsService _settingsService;

        public UpdateUserNotificationSettingsHandler(IHandler handler,
            IMapper mapper,
            IBusClient bus,
            IUserNotificationSettingsService settingsService)
        {
            _handler = handler;
            _mapper = mapper;
            _bus = bus;
            _settingsService = settingsService;
        }

        public async Task HandleAsync(UpdateUserNotificationSettings command)
        {
            await _handler
                .Run(async () =>
                {
                    var settings = _mapper.Map<UserNotificationSettings>(command);
                    await _settingsService.UpdateSettingsAsync(settings);
                })
                .OnSuccess(async () =>
                {
                    await _bus
                        .PublishAsync(new UserNotificationSettingsUpdated(
                            command.Request.Id, command.UserId));
                })
                .OnError(async (ex, logger) =>
                {
                    logger.Error(ex, "Error occured while updating user notification settings");
                    await _bus.PublishAsync(new UpdateUserNotificationSettingsRejected(
                        command.Request.Id, command.UserId, 
                        OperationCodes.CannotUpdateUserNotificationSettings, ex.Message));
                })
                .ExecuteAsync();
        }
    }
}