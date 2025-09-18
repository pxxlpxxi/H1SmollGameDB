using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Services
{
    internal static class ItemSmpStatements
    {
        public static string Create() => "INSERT INTO itemSimple (name, description, player_id) VALUES (@name, @description, @player_id)";
        public static string ReadAll() => "SELECT item_id, name, description, player_id FROM itemSimple";
        public static string Update() => "UPDATE itemSimple SET name = @name, description = @description, player_id = @player_id WHERE item_id = @id";
        public static string Delete() => "DELETE FROM itemSimple WHERE item_id = @id";

    }
}
