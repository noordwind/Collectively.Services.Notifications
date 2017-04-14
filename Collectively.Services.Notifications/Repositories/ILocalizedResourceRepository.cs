using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Repositories.Queries;
using MongoDB.Driver;

namespace Collectively.Services.Notifications.Repositories
{
    public interface ILocalizedResourceRepository
    {
        Task<Maybe<LocalizedResource>> GetAsync(string name, string culture);
    }

    public class LocalizedResourceRepository : ILocalizedResourceRepository
    {
        private readonly IMongoDatabase _database;

        public LocalizedResourceRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<LocalizedResource>> GetAsync(string name, string culture)
            => await _database.LocalizedResources().GetAsync(name, culture);
    }
}