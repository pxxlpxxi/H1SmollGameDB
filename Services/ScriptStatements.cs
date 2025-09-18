using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Services
{
    internal static class ScriptStatements
    {
        public static readonly string ItemComplexWithLocation = @"
        SELECT i.name AS [Lost Item], l.zone AS Location
        FROM itemComplex i
        JOIN location l ON i.location_id = l.location_id;
    ";

        public static readonly string ItemComplexWithOwner = @"
        SELECT 
            i.name AS Item,
            COALESCE(l.zone, u.username) AS [Possession of]
        FROM itemComplex i
        LEFT JOIN location l ON i.location_id = l.location_id
        LEFT JOIN player p ON i.player_id = p.player_id
        LEFT JOIN login u ON p.login_id = u.login_id
        ORDER BY [Possession of] asc;
    ";

        public static readonly string LoginWithPlayerStats = @"
        SELECT 
            l.login_id AS LoginID,
            l.username AS Username,
            p.[level] AS [Level],
            p.hp AS HP
        FROM [login] l
        JOIN player p ON l.login_id = p.login_id
        ORDER BY [level] DESC;
    ";

        public static readonly string LostItemsSimpleAndComplex = @"
        SELECT [name] AS 'Lost Item', [description]
        FROM itemSimple
        WHERE player_id IS NULL
        UNION
        SELECT [name], [description]
        FROM itemComplex
        WHERE player_id IS NULL;
    ";

        public static readonly string LostItemsWithZone = @"
        SELECT 
            s.[name] AS [Lost Item], 
            s.[description], 
            NULL AS [Zone]
        FROM itemSimple s
        WHERE s.player_id IS NULL
        UNION
        SELECT 
            c.[name] AS [Lost Item], 
            c.[description] AS [Description],
            l.[zone] AS [Zone]
        FROM itemComplex c
        LEFT JOIN [location] l ON c.location_id = l.location_id
        WHERE c.player_id IS NULL;
    ";

        public static readonly string LoginPlayerItems = @"
        WITH AllItems AS (
            SELECT player_id, [name]
            FROM itemSimple
            WHERE player_id IS NOT NULL
            UNION ALL
            SELECT player_id, [name]
            FROM itemComplex
            WHERE player_id IS NOT NULL
        )
        SELECT 
            l.login_id AS LoginID,
            l.username AS Username,
            p.[level] AS [Level],
            p.hp AS HP,
            ai.[name] AS Item
        FROM [login] l
        JOIN player p ON l.login_id = p.login_id
        LEFT JOIN AllItems ai ON p.player_id = ai.player_id
        ORDER BY p.[level] DESC;
    ";

        public static readonly string LoginPlayerItemsAggregated = @"
        WITH AllItems AS (
            SELECT player_id, [name]
            FROM itemSimple
            WHERE player_id IS NOT NULL
            UNION ALL
            SELECT player_id, [name]
            FROM itemComplex
            WHERE player_id IS NOT NULL
        )
        SELECT 
            l.login_id AS LoginID,
            l.username AS Username,
            p.[level] AS [Level],
            p.hp AS HP,
            STRING_AGG(ai.[name], ', ') AS Items
        FROM [login] l
        JOIN player p ON l.login_id = p.login_id
        LEFT JOIN AllItems ai ON p.player_id = ai.player_id
        GROUP BY l.login_id, l.username, p.[level], p.hp
        ORDER BY p.[level] DESC;
    ";

        public static readonly string PlayerPossessionsCount = @"
        WITH AllItems AS (
            SELECT item_id, player_id
            FROM itemSimple
            WHERE player_id IS NOT NULL
            UNION ALL
            SELECT item_id, player_id
            FROM itemComplex
            WHERE player_id IS NOT NULL
        )
        SELECT
            l.username AS Username,
            COUNT(i.item_id) AS [Possessions]
        FROM AllItems i
        JOIN player p ON p.player_id = i.player_id
        JOIN login l ON p.login_id = l.login_id
        GROUP BY l.username
        ORDER BY Possessions DESC;
    ";
    }
}
