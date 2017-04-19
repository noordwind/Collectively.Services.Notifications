using System.Threading.Tasks;
using Collectively.Common.Services;
using Collectively.Messages.Events;
using Collectively.Messages.Events.Remarks;
using Collectively.Services.Notifications.ServiceClients;
using Collectively.Services.Notifications.Services;

namespace Collectively.Services.Notifications.Handlers
{
    public class RemarkActionCanceledHandler : IEventHandler<RemarkActionCanceled>
    {
        private readonly IHandler _handler;
        private readonly IRemarkSubscribersService _subscribersService;
        private readonly IRemarkServiceClient _remarkServiceClient;

        public RemarkActionCanceledHandler(IHandler handler,
            IRemarkSubscribersService subscribersService,
            IRemarkServiceClient remarkServiceClient)
        {
            _handler = handler;
            _subscribersService = subscribersService;
            _remarkServiceClient = remarkServiceClient;
        }

        public async Task HandleAsync(RemarkActionCanceled @event)
        {
            await _handler
                .Run(async () =>
                {
                    var remark = await _remarkServiceClient.GetAsync(@event.RemarkId);
                    if (remark.HasValue)
                    {
                        var isAuthor = remark.Value.Author.UserId == @event.UserId;
                        if (isAuthor)
                            return;

                        var isInFavorites = remark.Value.UserFavorites.Contains(@event.UserId);
                        if (isInFavorites)
                            return;
                    }
                    await _subscribersService.RemoveSubscriberAsync(@event.RemarkId, @event.UserId);
                })
                .OnError((ex, logger) =>
                {
                    logger.Error(ex, $"Error occured while handling {@event.GetType().Name} event");
                })
                .ExecuteAsync();
        }
    }
}