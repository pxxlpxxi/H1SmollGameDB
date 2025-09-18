using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Services
{
    internal class StatementBuilder
    {
            public static string Insert(string tableName, IEnumerable<string> columns)
            {
                var cols = string.Join(", ", columns);
                var vals = string.Join(", ", columns.Select(col => "@" + col));

                return $"INSERT INTO [{tableName}] ({cols}) VALUES ({vals});";
            }

            public static string Select(string tableName, IEnumerable<string> whereColumns = null)
            {
                string whereClause = "";
                if (whereColumns != null && whereColumns.Any())
                {
                    whereClause = " WHERE " + string.Join(" AND ", whereColumns.Select(col => $"{col} = @{col}"));
                }

                return $"SELECT * FROM [{tableName}]{whereClause};";
            }

            public static string Update(string tableName, IEnumerable<string> updateColumns, IEnumerable<string> whereColumns)
            {
                string setClause = string.Join(", ", updateColumns.Select(col => $"{col} = @{col}"));
                string whereClause = string.Join(" AND ", whereColumns.Select(col => $"{col} = @Old{col}"));

                return $"UPDATE [{tableName}] SET {setClause} WHERE {whereClause};";
            }

            public static string Delete(string tableName, IEnumerable<string> whereColumns)
            {
                string whereClause = string.Join(" AND ", whereColumns.Select(col => $"{col} = @{col}"));
                return $"DELETE FROM [{tableName}] WHERE {whereClause};";
            }
        
    }
}
