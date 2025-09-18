using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Database
{
    /// <summary>
    /// Connection manager class
    /// </summary>
    internal class DBConnectionManager 
    {
        private readonly string _connectionString= "Server=localhost;Database=SmollGameDB;Trusted_Connection=true;TrustServerCertificate=true";
     /// <summary>
     /// Returns a new DB-connection
     /// </summary>
     /// <returns></returns>
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
