using Npgsql;
using System.Data;
using Dapper;
namespace sky.recovery.Services.Repository
{
    public class Database
    {

        private readonly string _connectionString;

        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }


    }
}
