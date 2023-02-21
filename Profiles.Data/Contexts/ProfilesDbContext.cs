using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Profiles.Data.Contexts
{
    public class ProfilesDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ProfilesDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ProfilesDbConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public IDbConnection CreateMasterConnection()
        {
            var connectionString = _configuration.GetConnectionString("MasterConnection");

            return new SqlConnection(connectionString);
        }
    }
}
