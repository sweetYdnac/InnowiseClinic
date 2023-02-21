using Dapper;
using Profiles.Data.Contexts;
using System.Data;

namespace Profiles.Data.Helpers
{
    public class DatabaseInitializer
    {
        private readonly ProfilesDbContext _db;

        public DatabaseInitializer(ProfilesDbContext db) => _db = db;

        public void CreateDatabase(string dbName)
        {
            var query = """
                            SELECT * FROM sys.databases
                            WHERE name = @name
                        """;

            var parameters = new DynamicParameters();
            parameters.Add("name", dbName, DbType.String);

            using (var connection = _db.CreateMasterConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                {
                    connection.Execute($"CREATE DATABASE \"{dbName}\"");
                }
            }
        }
    }
}
