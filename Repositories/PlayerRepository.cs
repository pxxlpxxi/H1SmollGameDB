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
        private readonly DBConnectionManager _db = new();

        private readonly DBHelper _helper = new();

        public bool CreatePlayer(Player player)
        {
            string statement = PlayerStatements.Insert();
            var parameters = new Dictionary<string, object>
            {
                ["@login_id"] = player.LoginID,
                ["@level"] = player.Level,
                ["@hp"] = player.HP
            };
            return _helper.QueryDataManipulation(statement, parameters);
        }
        public void Read()
        {
        }
        internal List<Player> GetAllPlayers()
        {
            var players = new List<Player>();
            //string statement = "SELECT player_id, login_id, level, hp FROM player";
            string statement = PlayerStatements.ReadAll();

            using SqlConnection conn = _db.CreateConnection(); // _db er DBConnectionManager
            using SqlCommand cmd = new SqlCommand(statement, conn);

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Player p = new Player
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("player_id")),
                        LoginID = reader.GetInt32(reader.GetOrdinal("login_id")),
                        Level = reader.GetInt32(reader.GetOrdinal("level")),
                        HP = reader.GetInt32(reader.GetOrdinal("hp"))
                    };
                    players.Add(p);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // using sørger for lukning af reader og connection

            return players;
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
            return _helper.QueryDataManipulation(statement, param);
        }
        public bool Delete(int id) {
            string statement = PlayerStatements.Delete();
            var param = new Dictionary<string, object> { { "@id", id } };
            return _helper.QueryDataManipulation(statement, param);
        }

    }
}
