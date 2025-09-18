using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Services
{
    internal static class MonsterTypeStatements
    {
        public static string Create() => "INSERT INTO monstertype (name) VALUES (@name)";
        public static string ReadAll() => "SELECT * FROM monstertype";
        public static string Update() => "UPDATE monstertype SET name = @name WHERE monstertype_id = @id";
        public static string Delete() => "DELETE FROM monstertype WHERE monstertype_id = @id";
    }
}
