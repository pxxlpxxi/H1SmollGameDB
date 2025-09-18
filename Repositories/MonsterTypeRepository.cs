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
    internal class MonsterTypeRepository
    {

        private readonly DBConnectionManager _db = new();
        private readonly DBQueryHelper _helper = new();

        public bool Create(MonsterType monster)
        {
            string statement = MonsterTypeStatements.Create();
            var parameters = new Dictionary<string, object>
            {
                { "@name", monster.Name }
            };
            return _helper.QueryDataManipulation(statement, parameters);
        }

        internal List<MonsterType> GetAll()
        {
            var monsters = new List<MonsterType>();
            string statement = MonsterTypeStatements.ReadAll();

            using SqlConnection conn = _db.CreateConnection();
            using SqlCommand cmd = new SqlCommand(statement, conn);

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MonsterType m = new MonsterType
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("monstertype_id")),
                        Name = reader.GetString(reader.GetOrdinal("name"))
                    };
                    monsters.Add(m);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return monsters;
        }

        public bool Update(MonsterType monster)
        {
            string statement = MonsterTypeStatements.Update();
            var parameters = new Dictionary<string, object>
            {
                { "@id", monster.Id },
                { "@name", monster.Name }
            };
            return _helper.QueryDataManipulation(statement, parameters);
        }

        public bool Delete(int id)
        {
            string statement = MonsterTypeStatements.Delete();
            var parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };
            return _helper.QueryDataManipulation(statement, parameters);
        }
    }

}

