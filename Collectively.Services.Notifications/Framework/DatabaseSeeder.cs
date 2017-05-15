using System.Collections.Generic;
using System.Threading.Tasks;
using Collectively.Common.Mongo;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Repositories.Queries;
using MongoDB.Driver;

namespace Collectively.Services.Notifications.Framework
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly IMongoDatabase _database;

        public DatabaseSeeder(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task SeedAsync()
        {
            if (await _database.LocalizedResources().AsQueryable().AnyAsync() == false)
            {
                var resources = new List<LocalizedResource>
                {
                    new LocalizedResource("category:praise", "en-gb", "praise"),
                    new LocalizedResource("category:praise", "pl-pl", "pochwała"),
                    new LocalizedResource("category:praise", "de-de", "Lob"),
                    new LocalizedResource("category:suggestion", "en-gb", "suggestion"),
                    new LocalizedResource("category:suggestion", "pl-pl", "sugestia"),
                    new LocalizedResource("category:suggestion", "de-de", "Vorschlag"),
                    new LocalizedResource("category:issue", "en-gb", "issue"),
                    new LocalizedResource("category:issue", "pl-pl", "problem"),
                    new LocalizedResource("category:issue", "de-de", "Problem"),
                    new LocalizedResource("category:defect", "en-gb", "defect"),
                    new LocalizedResource("category:defect", "pl-pl", "uszkodzenie"),
                    new LocalizedResource("category:defect", "de-de", "Defekt"),
                    new LocalizedResource("state:new", "en-gb", "new"),
                    new LocalizedResource("state:new", "pl-pl", "nowe"),
                    new LocalizedResource("state:new", "de-de", "neu"),
                    new LocalizedResource("state:processing", "en-gb", "processing"),
                    new LocalizedResource("state:processing", "pl-pl", "w trakcie"),
                    new LocalizedResource("state:processing", "de-de", "in Bearbeitung"),
                    new LocalizedResource("state:resolved", "en-gb", "resolved"),
                    new LocalizedResource("state:resolved", "pl-pl", "rozwiązane"),
                    new LocalizedResource("state:resolved", "de-de", "abgeschlossen"),
                    new LocalizedResource("state:renewed", "en-gb", "renewed"),
                    new LocalizedResource("state:renewed", "pl-pl", "przywrócone"),
                    new LocalizedResource("state:renewed", "de-de", "wieder geöffnet"),
                    new LocalizedResource("state:canceled", "en-gb", "canceled"),
                    new LocalizedResource("state:canceled", "pl-pl", "odrzucone"),
                    new LocalizedResource("state:canceled", "de-de", "abgebrochen")
                };

                await _database.LocalizedResources().InsertManyAsync(resources);
            }
        }
    }
}