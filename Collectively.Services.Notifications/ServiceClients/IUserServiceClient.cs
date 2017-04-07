using System.Threading.Tasks;
using Collectively.Common.ServiceClients;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Models;
using NLog;

namespace Collectively.Services.Notifications.ServiceClients
{
    public interface IUserServiceClient
    {
        Task<Maybe<User>> GetAsync(string userId);
    }

    public class UserServiceClient : IUserServiceClient
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IServiceClient _serviceClient;
        private readonly string _name;

        public UserServiceClient(IServiceClient serviceClient, string name)
        {
            _serviceClient = serviceClient;
            _name = name;
        }

        public async Task<Maybe<User>> GetAsync(string userId)
        {
            Logger.Debug($"Requesting GetAsync, userId:{userId}, type: {typeof(User).FullName}");
            return await _serviceClient.GetAsync<User>(_name, $"users/{userId}");
        }
    }
}