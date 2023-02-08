using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Offices.Data.Contexts
{
    public class OfficesDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public OfficesDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("OfficesDbConnection");
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

        public IDbConnection CreateMasterConnection() =>
            new NpgsqlConnection(_configuration.GetConnectionString("MasterConnection"));
    }
}
