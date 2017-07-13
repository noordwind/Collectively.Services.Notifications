using System.Threading.Tasks;
using Collectively.Common.Services;
using Collectively.Messages.Events;
using Collectively.Messages.Events.Remarks;
using Collectively.Services.Notifications.Services;

namespace Collectively.Services.Notifications.Handlers
{
    public class RemarkDeletedHandler : IEventHandler<RemarkDeleted>
    {
        private readonly IHandler _handler;
        private readonly IRemarkSubscribersService _subscribersService;

        public RemarkDeletedHandler(IHandler handler,
            IRemarkSubscribersService subscribersService)
        {
            _handler = handler;
            _subscribersService = subscribersService;
        }

        public async Task HandleAsync(RemarkDeleted @event)
        {
            await _handler
                .Run(async () =>
                {
                    await _subscribersService.RemoveSubscribersAsync(@event.RemarkId);
                })
                .OnError((ex, logger) =>
                {
                    logger.Error(ex, $"Error occured while handling {@event.GetType().Name} event");
                })
                .ExecuteAsync();
        }
    }
}