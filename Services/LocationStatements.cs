using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Services
{
    internal static class LocationStatements
    {
        public static string Create() => "INSERT INTO location (zone, description) VALUES (@zone, @description)";
        public static string ReadAll() => "SELECT location_id, zone, description FROM location";
        public static string Update() => "UPDATE location SET zone = @zone, description = @description WHERE location_id = @id";
        public static string Delete() => "DELETE FROM location WHERE location_id = @id";
    }
}
