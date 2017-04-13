using Collectively.Common.Host;
using Collectively.Messages.Commands.Notifications;
using Collectively.Messages.Events.Remarks;
using Collectively.Messages.Events.Users;
using Collectively.Services.Notifications.Framework;

namespace Collectively.Services.Notifications
{
    public class Program
    {
        static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10007)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq(queueName: typeof(Program).Namespace)
                .SubscribeToCommand<UpdateUserNotificationSettings>()
                .SubscribeToEvent<RemarkCreated>()
                .SubscribeToEvent<RemarkCanceled>()
                .SubscribeToEvent<RemarkDeleted>()
                .SubscribeToEvent<RemarkProcessed>()
                .SubscribeToEvent<RemarkRenewed>()
                .SubscribeToEvent<RemarkResolved>()
                .SubscribeToEvent<PhotosToRemarkAdded>()
                .SubscribeToEvent<CommentAddedToRemark>()
                .SubscribeToEvent<SignedUp>()
                .SubscribeToEvent<UsernameChanged>()
                .Build()
                .Run();
        }
    }
}
