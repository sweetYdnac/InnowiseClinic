using Dapper;
using Offices.Persistence.Contexts;
using System.Data;

namespace Offices.Persistence.Helpers
{
    public class DatabaseInitializer
    {
        private readonly OfficesDbContext _db;

        public DatabaseInitializer(OfficesDbContext db) => _db = db;

        public void CreateDatabase(string dbName)
        {
            var query =
                """
                    SELECT * FROM pg_database
                    WHERE datname = @name
                """;

            var parameters = new DynamicParameters();
            parameters.Add("name", dbName, DbType.String);

            using (var connection = _db.CreateMasterConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                {
                    connection.Execute($"CREATE DATABASE {dbName}");
                }
            }
        }
    }
}
