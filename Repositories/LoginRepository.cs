using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Abstractions;
using SmollGameDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmollGameDB.Database;

namespace SmollGameDB.Repositories
{
    /// <summary>
    /// Class for accessing data
    /// </summary>
    internal class LoginRepository
    {
        private readonly DBConnectionManager _db;
        private readonly SqlConnection _conn;
        private readonly DBService _service = new();
        public LoginRepository(DBConnectionManager db)
        {
            _db = db;
            _conn = db.Conn;
            
        }
        //private readonly string _currentUser;
        //public string CurrentUser => _currentUser;
        public void CreateLogin(string username, string password)
        {
            string statement;
            statement = $"INSERT INTO [login] VALUES('{username}','{password}')";
        }
        public void ReadAllUsers() { }
        public void UpdateLogin() { }
        public void DeleteLogin() { }

        private List<Login> GetAllLogins()
        {
            List<Login> logins = new();
            try {
                _conn.Open();

            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        internal bool ValidateUserLogin(string username, string password)
        {
            return _service.ValidateUserLogin();                    
        }
    }


}
