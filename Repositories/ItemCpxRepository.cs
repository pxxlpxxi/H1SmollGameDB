using Microsoft.Data.SqlClient;
using SmollGameDB.Database;
using SmollGameDB.Models;
using SmollGameDB.Services;
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
        private readonly DBConnectionManager _db = new();
        private readonly DBHelper _helper = new();

        public bool CreateItem(ItemCpx item)
        {
            string statement = ItemCpxStatements.Create();
            var parameters = new Dictionary<string, object>
            {
                ["@name"] = item.Name,
                ["@type"] = item.Type ?? (object)DBNull.Value,
                ["@description"] = item.Description ?? (object)DBNull.Value,
                ["@player_id"] = item.PlayerId == 0 ? (object)DBNull.Value : item.PlayerId,
                ["@location_id"] = item.LocationId == 0 ? (object)DBNull.Value : item.LocationId
            };
            return _helper.QueryDataManipulation(statement, parameters);
        }

        internal List<ItemCpx> GetAllItems()
        {
            var items = new List<ItemCpx>();
            string statement = ItemCpxStatements.ReadAll();

            using SqlConnection conn = _db.CreateConnection();
            using SqlCommand cmd = new SqlCommand(statement, conn);

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ItemCpx item = new ItemCpx
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("item_id")),
                        Name = reader.GetString(reader.GetOrdinal("name")),
                        Type = reader.IsDBNull(reader.GetOrdinal("type")) ? null : reader.GetString(reader.GetOrdinal("type")),
                        Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description")),
                        PlayerId = reader.IsDBNull(reader.GetOrdinal("player_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("player_id")),
                        LocationId = reader.IsDBNull(reader.GetOrdinal("location_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("location_id"))
                    };
                    items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return items;
        }

        public bool UpdateItem(ItemCpx item)
        {
            string statement = ItemCpxStatements.Update();
            var parameters = new Dictionary<string, object>
            {
                ["@id"] = item.Id,
                ["@name"] = item.Name,
                ["@type"] = item.Type ?? (object)DBNull.Value,
                ["@description"] = item.Description ?? (object)DBNull.Value,
                ["@player_id"] = item.PlayerId == 0 ? (object)DBNull.Value : item.PlayerId,
                ["@location_id"] = item.LocationId == 0 ? (object)DBNull.Value : item.LocationId
            };
            return _helper.QueryDataManipulation(statement, parameters);
        }
        public bool ItemExists(int id) {
        
            string statement = ItemCpxStatements.Exists();

            var parameters = new Dictionary<string, object> {
                { "@id", id}
            };
            DBHelper helper = new();
            return helper.QueryDataReader(statement, parameters);
        }
        
        public (bool itemExists, bool sqlSuccess) DeleteItem(int id)
        {
            bool exists = ItemExists(id);

            string statement = ItemCpxStatements.Delete();
            var parameters = new Dictionary<string, object> { ["@id"] = id };
            return (exists, _helper.QueryDataManipulation(statement, parameters));
        }

    }
}

