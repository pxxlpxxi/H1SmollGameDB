using Microsoft.Data.SqlClient;
using SmollGameDB.Database;
using SmollGameDB.Models;
using SmollGameDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Repositories
{

    internal class LocationRepository
    {
        private readonly DBConnectionManager _db = new();
        private readonly DBHelper _helper = new();

        public bool CreateLocation(Location location)
        {
            string statement = LocationStatements.Create();
            var parameters = new Dictionary<string, object>
            {
                ["@zone"] = location.Zone,
                ["@description"] = location.Description ?? (object)DBNull.Value
            };
            return _helper.QueryDataManipulation(statement, parameters);
        }

        internal List<Location> GetAllLocations()
        {
            var locations = new List<Location>();
            string statement = LocationStatements.ReadAll();

            using SqlConnection conn = _db.CreateConnection();
            using SqlCommand cmd = new SqlCommand(statement, conn);

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Location loc = new Location
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("location_id")),
                        Zone = reader.GetString(reader.GetOrdinal("zone")),
                        Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description"))
                    };
                    locations.Add(loc);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return locations;
        }

        public bool UpdateLocation(Location location)
        {
            string statement = LocationStatements.Update();
            var parameters = new Dictionary<string, object>
            {
                ["@id"] = location.Id,
                ["@zone"] = location.Zone,
                ["@description"] = location.Description ?? (object)DBNull.Value
            };
            return _helper.QueryDataManipulation(statement, parameters);
        }

        public bool DeleteLocation(int id)
        {
            string statement = LocationStatements.Delete();
            var parameters = new Dictionary<string, object> { ["@id"] = id };
            return _helper.QueryDataManipulation(statement, parameters);
        }
    }
}

