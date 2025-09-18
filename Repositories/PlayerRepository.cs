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
    internal class PlayerRepository
    {
        private readonly DBHelper _db = new();

        public bool CreatePlayer(Player player)
        {
            string statement = PlayerStatements.Insert();
            var parameters = new Dictionary<string, object>
            {
                ["@login_id"] = player.LoginID,
                ["@level"] = player.Level,
                ["@hp"] = player.HP
            };
            return _db.QueryDataManipulation(statement, parameters);
        }
        public void Read()
        {
        }
        public List<Player> GetAllPlayers()
        {
            string statement = "SELECT player_id, login_id, level, hp FROM player";
            return _db.QueryPlayers(statement, null);
        }
        public bool UpdatePlayer(Player player)
        {
            //hent sql statement
            string statement = PlayerStatements.Update();

            //map statement-parametre
            var param = new Dictionary<string, object>
            {
                {"@id", player.ID},
                {"@level", player.Level},
                {"@hp", player.HP}
            };
            //returner om sql-forespørgsel lykkedes
            return _db.QueryDataManipulation(statement, param);
        }
        public bool Delete(int id) {
            string statement = PlayerStatements.Delete();
            var param = new Dictionary<string, object> { { "@id", id } };
            return _db.QueryDataManipulation(statement, param);
        }

    }
}
