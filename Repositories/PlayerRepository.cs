using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmollGameDB.Services;

namespace SmollGameDB.Repositories
{
    internal class PlayerRepository
    {
        public void Create() { }
        public void Read() {
            string statement = StatementBuilder.Select("player", new[] { "username" }); // fx til login
        }
        public void Update() { }
        public void Delete() { }

    }
}
