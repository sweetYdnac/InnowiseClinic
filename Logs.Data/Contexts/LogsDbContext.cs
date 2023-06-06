using Logs.Data.Configurations;
using Logs.Data.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Logs.Data.Contexts
{
    public class LogsDbContext
    {
        public IMongoCollection<Log> Logs;

        public LogsDbContext(MonboDbConfigurations config)
        {
            LogEntityConfiguration.Configure();

            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);
            Logs = database.GetCollection<Log>(config.LogsCollectionName);
        }
    }
}
