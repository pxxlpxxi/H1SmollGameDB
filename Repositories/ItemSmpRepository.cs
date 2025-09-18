using Microsoft.Data.SqlClient;
using SmollGameDB.Database;
using SmollGameDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmollGameDB.Services;

namespace SmollGameDB.Repositories
{
    internal class ItemSmpRepository
    {
        private readonly DBConnectionManager _db = new();
        private readonly DBHelper _helper = new();

        public bool CreateItem(ItemSmp item)
        {
            string statement = ItemSmpStatements.Create();
            var parameters = new Dictionary<string, object>
            {
                ["@name"] = item.Name,
                ["@description"] = item.Description ?? (object)DBNull.Value,
                ["@player_id"] = item.PlayerId == 0 ? (object)DBNull.Value : item.PlayerId
            };
            return _helper.QueryDataManipulation(statement, parameters);
        }

        internal List<ItemSmp> GetAllItems()
        {
            var items = new List<ItemSmp>();
            string statement = ItemSmpStatements.ReadAll();

            using SqlConnection conn = _db.CreateConnection();
            using SqlCommand cmd = new SqlCommand(statement, conn);

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ItemSmp item = new ItemSmp
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("item_id")),
                        Name = reader.GetString(reader.GetOrdinal("name")),
                        Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description")),
                        PlayerId = reader.IsDBNull(reader.GetOrdinal("player_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("player_id"))
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

        public bool UpdateItem(ItemSmp item)
        {
            string statement = ItemSmpStatements.Update();
            var parameters = new Dictionary<string, object>
            {
                ["@id"] = item.Id,
                ["@name"] = item.Name,
                ["@description"] = item.Description ?? (object)DBNull.Value,
                ["@player_id"] = item.PlayerId == 0 ? (object)DBNull.Value : item.PlayerId
            };
            return _helper.QueryDataManipulation(statement, parameters);
        }

        public bool DeleteItem(int id)
        {
            string statement = ItemSmpStatements.Delete();
            var parameters = new Dictionary<string, object> { ["@id"] = id };
            return _helper.QueryDataManipulation(statement, parameters);
        }
    }
}

