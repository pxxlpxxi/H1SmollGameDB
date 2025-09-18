using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Services
{
    internal static class ItemCpxStatements
    {
        public static string Create() => "INSERT INTO itemComplex (name, type, description, player_id, location_id) VALUES (@name, @type, @description, @player_id, @location_id)";
        public static string ReadAll() => "SELECT item_id, name, type, description, player_id, location_id FROM itemComplex";
        public static string Update() => "UPDATE itemComplex SET name = @name, type = @type, description = @description, player_id = @player_id, location_id = @location_id WHERE item_id = @id";
        public static string Delete() => "DELETE FROM itemComplex WHERE item_id = @id";
        public static string Exists() => "SELECT 1 FROM itemComplex WHERE item_id = @id";
    }
}
