using Collectively.Common.Host;
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
                .Build()
                .Run();
        }
    }
}
