using System;
using System.Threading.Tasks;
using Collectively.Common.ServiceClients;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Models;
using NLog;

namespace Collectively.Services.Notifications.ServiceClients
{
    public class RemarkServiceClient : IRemarkServiceClient
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IServiceClient _serviceClient;
        private readonly string _name;

        public RemarkServiceClient(IServiceClient serviceClient, string name)
        {
            _serviceClient = serviceClient;
            _name = name;
        }

        public async Task<Maybe<Remark>> GetAsync(Guid id)
        {
            Logger.Debug($"Requesting GetAsync, id:{id}, type: {typeof(Remark).FullName}");
            return await _serviceClient
                .GetAsync<Remark>(_name, $"remarks/{id}");
        }
    }
}