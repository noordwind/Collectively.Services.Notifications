using System;
using System.Threading.Tasks;
using AutoMapper;
using Collectively.Common.Services;
using Collectively.Common.Types;
using Collectively.Messages.Commands;
using Collectively.Messages.Commands.Notifications;
using Collectively.Messages.Events.Notifications;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Repositories;
using RawRabbit;

namespace Collectively.Services.Notifications.Handlers
{
    public class UpdateUserNotificationSettingsHandler : ICommandHandler<UpdateUserNotificationSettings>
    {
        private readonly IHandler _handler;
        private readonly IBusClient _bus;
        private readonly IMapper _mapper;
        private readonly IUserNotificationSettingsRepository _settingsRepository;

        public UpdateUserNotificationSettingsHandler(IHandler handler,
            IBusClient bus,
            IMapper mapper,
            IUserNotificationSettingsRepository settingsRepository)
        {
            _handler = handler;
            _bus = bus;
            _mapper = mapper;
            _settingsRepository = settingsRepository;
        }

        public async Task HandleAsync(UpdateUserNotificationSettings command)
        {
            await _handler
                .Run(async () =>
                {
                    var settings = await _settingsRepository.GetByIdAsync(command.UserId);
                    if (settings.HasNoValue)
                    {
                        settings = _mapper.Map<UserNotificationSettings>(command);
                        await _settingsRepository.AddAsync(settings.Value);
                    }
                    else
                    {
                        settings.Value.EmailSettings = _mapper
                            .Map<NotificationSettings>(command.EmailSettings);
                        settings.Value.PushSettings = _mapper
                            .Map<NotificationSettings>(command.PushSettings);
                        await _settingsRepository.EditAsync(settings.Value);
                    }
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