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
    internal class DBConnectionManager : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _conn;
        public SqlConnection Conn => _conn;
           
        public DBConnectionManager()
        {
            _connectionString = "Server=localhost;Database=SmollGameDB;Trusted_Connection=true;TrustServerCertificate=true";
            _conn = new(_connectionString);

            // -> find ud af senere: skal connection åbnes her med det samme, eller skal Open() bare kaldes
        }

        /// <summary>
        /// Already exists in class but ????
        /// </summary>
        public void Open()
        {
            if (_conn.State != System.Data.ConnectionState.Open)
            {
                _conn.Open();
            }
        }

        /// <summary>
        /// Close() esisterer allerede men ???
        /// </summary>
        public void Close()
        {
            if (_conn.State != System.Data.ConnectionState.Closed)
            {
                _conn.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (_conn != null)
            {
                if (_conn.State != System.Data.ConnectionState.Closed)
                {
                    _conn.Close();
                }
                _conn.Dispose();
                _conn = null;
            }
        }
        

    }
}
