using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Models
{
    internal class ScriptModels
    {
        public class ItemWithLocation
        {
            public string LostItem { get; set; }
            public string Location { get; set; }
        }

        public class ItemWithOwner
        {
            public string Item { get; set; }
            public string PossessionOf { get; set; }
        }

        public class LoginPlayerStats
        {
            public int LoginID { get; set; }
            public string Username { get; set; }
            public int Level { get; set; }
            public int HP { get; set; }
        }

        public class LostItemSimpleComplex
        {
            public string LostItem { get; set; }
            public string Description { get; set; }
            public string Zone { get; set; } // Kan være null
        }

        public class LoginPlayerItem
        {
            public int LoginID { get; set; }
            public string Username { get; set; }
            public int Level { get; set; }
            public int HP { get; set; }
            public string Item { get; set; }
        }

        public class LoginPlayerItemsAggregated
        {
            public int LoginID { get; set; }
            public string Username { get; set; }
            public int Level { get; set; }
            public int HP { get; set; }
            public string Items { get; set; }
        }

        public class PlayerPossessionsCount
        {
            public string Username { get; set; }
            public int Possessions { get; set; }
        }

    }
}
