using Logs.Data.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Logs.Data.Configurations
{
    public static class LogEntityConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Log>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);

                cm.MapIdProperty(c => c.Id).SetIdGenerator(ObjectIdGenerator.Instance);
                cm.MapMember(c => c.DateTime).SetIsRequired(true);
                cm.MapMember(c => c.ApiName).SetIsRequired(true);
                cm.MapMember(c => c.Route).SetIsRequired(true);
                cm.MapMember(c => c.Code).SetIsRequired(true);
                cm.MapMember(c => c.Message).SetIsRequired(true);
                cm.MapMember(c => c.Details).SetIsRequired(true);
            });
        }
    }
}
