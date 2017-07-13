using Autofac;
using Collectively.Common.ServiceClients;
using Collectively.Services.Notifications.ServiceClients;

namespace Collectively.Services.Notifications.Framework.IoC
{
    public class ServiceClientsModule : ServiceClientsModuleBase
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterService<RemarkServiceClient, IRemarkServiceClient>(builder, "remarks");
            RegisterService<UserServiceClient, IUserServiceClient>(builder, "users");
        }
    }
}