using System.Threading.Tasks;
using Collectively.Common.Services;
using Collectively.Messages.Events;
using Collectively.Messages.Events.Remarks;
using Collectively.Services.Notifications.Services;

namespace Collectively.Services.Notifications.Handlers
{
    public class PhotosToRemarkAddedHandler : IEventHandler<PhotosToRemarkAdded>
    {
        private readonly IHandler _handler;
        private readonly IRemarkSubscribersService _subscribersService;
        private readonly INotificationService _notificationService;

        public PhotosToRemarkAddedHandler(IHandler handler,
            IRemarkSubscribersService subscribersService,
            INotificationService notificationService)
        {
            _handler = handler;
            _subscribersService = subscribersService;
            _notificationService = notificationService;
        }

        public async Task HandleAsync(PhotosToRemarkAdded @event)
        {
            await _handler
                .Run(async () =>
                {
                    await _subscribersService.AddSubscriberAsync(@event.RemarkId, @event.UserId);
                    await _notificationService.NotifyPhotosAddedAsync(@event.RemarkId);
                })
                .OnError((ex, logger) =>
                {
                    logger.Error(ex, $"Error occured while handling {@event.GetType().Name} event");
                })
                .ExecuteAsync();
        }
    }
}