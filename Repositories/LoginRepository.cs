using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Abstractions;
using SmollGameDB.Database;
using SmollGameDB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Repositories
{
    /// <summary>
    /// Class for accessing data
    /// </summary>
    internal class LoginRepository
    {
        private readonly DBConnectionManager _db = new();
        private Login? _currentUser;
        public Login? CurrentUser => _currentUser;

        private string CreateLoginStatement() //helper
        {
            return "INSERT INTO [login] (username, password) VALUES (@Username, @Password);";
        }
        private string ValidateLoginStatement() //helper
        {
            return "SELECT login_id, username, password FROM [login] WHERE " +
                "username COLLATE SQL_Latin1_General_CP1_CI_AS = @Username" + //case insensitice
                " AND " +
                "password COLLATE SQL_Latin1_General_CP1_CS_AS = @Password"; //case sensitive
        }
        private string ReadUsernameStatement()
        {
            //case insensitive
            return "SELECT username FROM [login] WHERE username = @Username";
        }
        private string DeleteLoginStatement()
        {
            return "DELETE FROM [login] WHERE username = @Username";
        }
        internal Login? ValidateUserLogin(string username, string password)
        {
            string statement = ValidateLoginStatement();

            using SqlConnection conn = _db.CreateConnection();

            try
            {
                using (SqlCommand cmd = new(statement, conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = cmd.ExecuteReader();

                    bool userFound = false;
                    if (userFound = reader.Read())
                    {
                        SetCurrentUser(reader);
                    }
                    reader.Close();
                    return CurrentUser;
                }
            }
            finally
            {
                conn.Close();
            }
        }
        private void SetCurrentUser(SqlDataReader reader)
        {
            _currentUser = new()
            {
                Id = reader.GetInt32(reader.GetOrdinal("login_id")),
                Username = reader.GetString(reader.GetOrdinal("username")),
                Password = reader.GetString(reader.GetOrdinal("password"))
            };
        }
        internal void UserLogout() //reset current user
        {
            _currentUser = null;
        }

        internal bool UsernameExists(string username)
        {
            string statement = ReadUsernameStatement();

            var parameters = new Dictionary<string, object> {
                { "@Username", username}
            };
            DBQueryHelper helper = new();
            return helper.QueryDataReader(statement, parameters);
        }
        public bool CreateLogin(string username, string password)
        {

            //hvis vi skal bruge et objekt
            Login login = new();
            login.Username = username;
            login.Password = password;

            string statement = CreateLoginStatement();
            var parameters = new Dictionary<string, object> {
                { "@Username", username},
                { "@Password", password}
            };
            DBQueryHelper helper = new();
            return helper.QueryDataManipulation(statement, parameters);
        }

        internal bool UpdateLogin(string oldUsername, string newUsername, string? newPassword)
        {
            var SqlBuilder = new StringBuilder("UPDATE [login] SET ");
            var parameters = new Dictionary<string, object>();

            if (oldUsername != newUsername)
            {
                SqlBuilder.Append("username = @NewUsername");
                parameters["@NewUsername"] = newUsername;
            }
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                if (parameters.Count > 0)
                {
                    SqlBuilder.Append(", ");
                }

                SqlBuilder.Append("password = @NewPassword");
                parameters["@NewPassword"] = newPassword;
            }

            SqlBuilder.Append(" WHERE username = @OldUsername");
            parameters["@OldUsername"] = oldUsername;

            string statement = SqlBuilder.ToString();
            DBQueryHelper helper = new();
            return helper.QueryDataManipulation(statement, parameters);
        }

        internal bool DeleteLogin(string username)
        {
            string statement = DeleteLoginStatement();
            var parameters = new Dictionary<string, object>
            {
                { "@Username", username }
            };
            DBQueryHelper helper = new();
            return helper.QueryDataManipulation(statement,parameters);
        }


    }

}
