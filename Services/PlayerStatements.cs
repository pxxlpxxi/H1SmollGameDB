using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Services
{
    internal static class PlayerStatements
    {
        public static string Create() => "INSERT INTO player (login_id, level, hp) VALUES (@login_id, @level, @hp)";
        public static string ReadAll() => "SELECT * FROM player";
        public static string Update() => "UPDATE player SET level = @level, hp = @hp WHERE player_id = @id";
        public static string Insert() => "INSERT INTO player (login_id, level, hp) VALUES (@login_id, @level, @hp)";
        public static string Delete() => "DELETE FROM player WHERE player_id = @id";
    }
}
