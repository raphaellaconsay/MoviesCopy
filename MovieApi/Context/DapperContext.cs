using Microsoft.Data.SqlClient;
using System.Data;

namespace MovieApi.Context
{
    public class DapperContext
    {
        private string _connectionString;

        /// <summary>
        /// Create new instance of DapperContext
        /// </summary>
        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServer");
        }

        /// <summary>
        /// Creates a new connection to the database
        /// </summary>
        /// <returns>Returns the database connection</returns>
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
