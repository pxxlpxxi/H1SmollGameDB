using Microsoft.Data.SqlClient;
using SmollGameDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Database
{
    /// <summary>
    /// helper class for handling queries 
    /// </summary>
    internal class DBQueryHelper
    {
        private readonly DBConnectionManager _db = new();

        public bool QueryDataManipulation(string statement, Dictionary<string, object> parameters)
        {
             SqlConnection conn = _db.CreateConnection();

            SqlCommand cmd = new SqlCommand(statement, conn);
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
            }

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
      
        public bool QueryDataReader(string statement, Dictionary<string, object>? parameters)
        {
            using SqlConnection conn = _db.CreateConnection();

            using SqlCommand cmd = new SqlCommand(statement, conn);
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
            }
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                return reader.Read(); //true hvis søgningen findes
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public SqlDataReader? QueryDataReaderReturn(string statement, Dictionary<string, object>? parameters) {
            using SqlConnection conn = _db.CreateConnection();

            using SqlCommand cmd = new SqlCommand(statement, conn);
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
            }
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                return cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
//private readonly Database _db;
//private readonly SqlConnection _conn;
//public DBService() { }
//public DBService(DBConnectionManager db)
//{
//    _db = db;
//    _conn = db.Conn;
//}
//public void QueryDataManipulation(string statement)
//{
//    try
//    {
//        using (SqlConnection conn = new SqlConnection(_conn))
//        {
//        _conn.Open();
//        SqlCommand cmd = new SqlCommand(statement, _conn);
//        cmd.ExecuteNonQuery();
//        Console.WriteLine("Kommando udført: " + statement);
//        _conn.Close();
//        _conn.Dispose();
//        }
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }
//}
//public void CreateCommand(string sql, SqlParameter param)
//{

//}

//public List<Login> GetAllLogins()
//{
//    List<Login> logins = new();

//    return logins;
//}
//internal bool ValidateUserLogin(string username, string password)
//{
//    using var conn = _db;
//    string sql = "SELECT COUNT(*) FROM Logins WHERE Username = @Username AND Password = @Password";

//    using (SqlCommand cmd = new SqlCommand(sql, _conn))
//    {
//        cmd.Parameters.AddWithValue("@Username", username);
//        cmd.Parameters.AddWithValue("@Password", password);

//        _conn.Open();
//        int result = (int)cmd.ExecuteScalar();
//        _conn.Close();

//        return result > 0;
//    }
//}


//public List<Player> GetAllPlayers()
//{
//    List<Player> players = new();
//    return players;
//}
//public List<ItemCpx> GetAllItemCpx()
//{
//    List<ItemCpx> items = new();
//    return items;
//}
//public List<ItemSmp> GetAllItemSmp()
//{
//    List<ItemSmp> items = new();
//    return items;
//}
//public List<MonsterType> GetAllMonsterTypes()
//{
//    List<MonsterType> monsters = new();
//    return monsters;
//}
////public void QueryDataReader(string statement)
////{
////    try
////    {
////        using (SqlConnection conn = new SqlConnection(ConnectionString))
////        {
////            conn.Open();
////            SqlCommand cmd = new SqlCommand(statement, conn);

////            SqlDataReader reader = cmd.ExecuteReader();
////            while (reader.Read())
////            {
////                for (int i = 0; i < reader.FieldCount; i++)
////                {
////                    Console.Write(reader[i]);
////                }
////                Console.WriteLine();
////            }
////            reader.Close();
////        }
////    }
////    catch (Exception e)
////    {
////        Console.WriteLine(e.Message);
////    }
////}

////public void QueryDataReader(string statement)
////{
////    try
////    {
////        using (SqlConnection conn = new SqlConnection(ConnectionString))
////        {
////            conn.Open();
////            SqlCommand cmd = new SqlCommand(statement, conn);

////            SqlDataReader reader = cmd.ExecuteReader();
////            while (reader.Read())
////            {
////                for (int i = 0; i < reader.FieldCount; i++)
////                {
////                    Console.Write(reader[i]);
////                }
////                Console.WriteLine();
////            }
////            reader.Close();
////        }
////    }
////    catch (Exception e)
////    {
////        Console.WriteLine(e.Message);
////    }
////}

