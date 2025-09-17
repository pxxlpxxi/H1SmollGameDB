using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Models
{
    internal class Player
    {
        public int Id { get; set; }
        public int Login { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
    }
}
