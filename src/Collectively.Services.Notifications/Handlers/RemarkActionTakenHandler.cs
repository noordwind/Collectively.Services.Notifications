using System.Threading.Tasks;
using Collectively.Common.Services;
using Collectively.Messages.Events;
using Collectively.Messages.Events.Remarks;
using Collectively.Services.Notifications.Services;

namespace Collectively.Services.Notifications.Handlers
{
    public class RemarkActionTakenHandler : IEventHandler<RemarkActionTaken>
    {
        private readonly IHandler _handler;
        private readonly IRemarkSubscribersService _subscribersService;

        public RemarkActionTakenHandler(IHandler handler,
            IRemarkSubscribersService subscribersService)
        {
            _handler = handler;
            _subscribersService = subscribersService;
        }

        public async Task HandleAsync(RemarkActionTaken @event)
        {
            await _handler
                .Run(async () =>
                {
                    await _subscribersService.AddSubscriberAsync(@event.RemarkId, @event.UserId);
                })
                .OnError((ex, logger) =>
                {
                    logger.Error(ex, $"Error occured while handling {@event.GetType().Name} event");
                })
                .ExecuteAsync();
        }
    }
}