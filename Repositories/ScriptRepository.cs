using Microsoft.Data.SqlClient;
using SmollGameDB.Database;
using SmollGameDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SmollGameDB.Models.ScriptModels;

namespace SmollGameDB.Repositories
{
    
        internal class ScriptRepository
        {
            private readonly DBConnectionManager _db = new();
            private readonly DBQueryHelper _helper = new();

            // Helper metode til sikkert at hente strings der kan være null
            private string GetNullableString(SqlDataReader reader, string columnName)
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
            }

            public List<ItemWithLocation> GetItemComplexWithLocation()
            {
                var list = new List<ItemWithLocation>();
                string statement = ScriptStatements.ItemComplexWithLocation;

                using SqlConnection conn = _db.CreateConnection();
                using SqlCommand cmd = new SqlCommand(statement, conn);

                try
                {
                    conn.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new ItemWithLocation
                        {
                            LostItem = GetNullableString(reader, "Lost Item"),
                            Location = GetNullableString(reader, "Location")
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return list;
            }

            public List<ItemWithOwner> GetItemComplexWithOwner()
            {
                var list = new List<ItemWithOwner>();
                string statement = ScriptStatements.ItemComplexWithOwner;

                using SqlConnection conn = _db.CreateConnection();
                using SqlCommand cmd = new SqlCommand(statement, conn);

                try
                {
                    conn.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new ItemWithOwner
                        {
                            Item = GetNullableString(reader, "Item"),
                            PossessionOf = GetNullableString(reader, "Possession of")
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return list;
            }

            public List<LoginPlayerStats> GetLoginWithPlayerStats()
            {
                var list = new List<LoginPlayerStats>();
                string statement = ScriptStatements.LoginWithPlayerStats;

                using SqlConnection conn = _db.CreateConnection();
                using SqlCommand cmd = new SqlCommand(statement, conn);

                try
                {
                    conn.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new LoginPlayerStats
                        {
                            LoginID = reader.GetInt32(reader.GetOrdinal("LoginID")),
                            Username = GetNullableString(reader, "Username"),
                            Level = reader.GetInt32(reader.GetOrdinal("Level")),
                            HP = reader.GetInt32(reader.GetOrdinal("HP"))
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return list;
            }

            public List<LostItemSimpleComplex> GetLostItemsSimpleAndComplex()
            {
                var list = new List<LostItemSimpleComplex>();
                string statement = ScriptStatements.LostItemsWithZone;

                using SqlConnection conn = _db.CreateConnection();
                using SqlCommand cmd = new SqlCommand(statement, conn);

                try
                {
                    conn.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new LostItemSimpleComplex
                        {
                            LostItem = GetNullableString(reader, "Lost Item"),
                            Description = GetNullableString(reader, "Description"),
                            Zone = GetNullableString(reader, "Zone")
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return list;
            }

            public List<LoginPlayerItem> GetLoginPlayerItems()
            {
                var list = new List<LoginPlayerItem>();
                string statement = ScriptStatements.LoginPlayerItems;

                using SqlConnection conn = _db.CreateConnection();
                using SqlCommand cmd = new SqlCommand(statement, conn);

                try
                {
                    conn.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new LoginPlayerItem
                        {
                            LoginID = reader.GetInt32(reader.GetOrdinal("LoginID")),
                            Username = GetNullableString(reader, "Username"),
                            Level = reader.GetInt32(reader.GetOrdinal("Level")),
                            HP = reader.GetInt32(reader.GetOrdinal("HP")),
                            Item = GetNullableString(reader, "Item")
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return list;
            }

            public List<LoginPlayerItemsAggregated> GetLoginPlayerItemsAggregated()
            {
                var list = new List<LoginPlayerItemsAggregated>();
                string statement = ScriptStatements.LoginPlayerItemsAggregated;

                using SqlConnection conn = _db.CreateConnection();
                using SqlCommand cmd = new SqlCommand(statement, conn);

                try
                {
                    conn.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new LoginPlayerItemsAggregated
                        {
                            LoginID = reader.GetInt32(reader.GetOrdinal("LoginID")),
                            Username = GetNullableString(reader, "Username"),
                            Level = reader.GetInt32(reader.GetOrdinal("Level")),
                            HP = reader.GetInt32(reader.GetOrdinal("HP")),
                            Items = GetNullableString(reader, "Items")
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return list;
            }

            public List<PlayerPossessionsCount> GetPlayerPossessionsCount()
            {
                var list = new List<PlayerPossessionsCount>();
                string statement = ScriptStatements.PlayerPossessionsCount;

                using SqlConnection conn = _db.CreateConnection();
                using SqlCommand cmd = new SqlCommand(statement, conn);

                try
                {
                    conn.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new PlayerPossessionsCount
                        {
                            Username = GetNullableString(reader, "Username"),
                            Possessions = reader.GetInt32(reader.GetOrdinal("Possessions"))
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return list;
            }
        }
    }