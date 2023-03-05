using Dapper;
using Offices.Data.Contexts;

namespace Offices.Data.Helpers
{
    public class DatabaseInitializer
    {
        private readonly OfficesDbContext _db;

        public DatabaseInitializer(OfficesDbContext db) => _db = db;

        public void CreateDatabase()
        {
            var query =
                        $"""
                            SELECT * FROM pg_database
                            WHERE datname = 'OfficesDb'
                        """;

            using (var connection = _db.CreateMasterConnection())
            {
                var records = connection.Query(query);

                if (!records.Any())
                {
                    connection.Execute(
                        """
                            CREATE DATABASE "OfficesDb"
                        """);
                }
            }
        }
    }
}
