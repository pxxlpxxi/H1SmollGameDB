using Microsoft.Data.SqlClient;
using SmollGameDB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Repositories
{
    internal class ItemCpxRepository
    {
        private readonly string _connectionString;
        public string ConnectionString => _connectionString;
        //public ItemCpxRepository(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        // Hent alle items
        public List<ItemCpx> GetAllItems()
        {
            var items = new List<ItemCpx>();
            string sql = "SELECT Id, Name, Type, Description, PlayerId, LocationId FROM Items";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new ItemCpx
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Description = reader.GetString(3),
                            PlayerId = reader.GetInt32(4),
                            LocationId = reader.GetInt32(5)
                        });
                    }
                }
            }
            return items;
        }

        // Tilføj et item
        public void AddItem(ItemCpx item)
        {
            string sql = @"
            INSERT INTO Items (Name, Type, Description, PlayerId, LocationId)
            VALUES (@Name, @Type, @Description, @PlayerId, @LocationId);
            SELECT CAST(scope_identity() AS int);
        ";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name;
                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = item.Type;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = item.Description;
                cmd.Parameters.Add("@PlayerId", SqlDbType.Int).Value = item.PlayerId;
                cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = item.LocationId;

                conn.Open();
                // Sæt den auto-genererede Id tilbage til item.Id
                item.Id = (int)cmd.ExecuteScalar();
            }
        }

        // Opdater et item
        public void UpdateItem(ItemCpx item)
        {
            string sql = @"
            UPDATE Items 
            SET Name = @Name, Type = @Type, Description = @Description, PlayerId = @PlayerId, LocationId = @LocationId
            WHERE Id = @Id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name;
                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = item.Type;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = item.Description;
                cmd.Parameters.Add("@PlayerId", SqlDbType.Int).Value = item.PlayerId;
                cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = item.LocationId;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = item.Id;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Slet et item (ekstra)
        public void DeleteItem(int id)
        {
            string sql = "DELETE FROM Items WHERE Id = @Id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }


}

