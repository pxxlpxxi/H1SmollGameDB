using SmollGameDB.Database;
using SmollGameDB.Repositories;
using SmollGameDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.ConsoleUI
{
    internal class ScriptTables
    {
        private readonly ScriptRepository _repo = new();
        private readonly UIHelper _helper = new();

        public void Run()
        {
            //menu list
            string[] menu = {
                "Choose a script statement",
                " ",
                "[1] List itemComplex with location",
                "[2] List itemComplex with owner",
                "[3] Show login with player stats",
                "[4] List lost items (simple + complex) with zone",
                "[5] Show login-player with individual items",
                "[6] Show login-player items",
                "[7] Show login-player with aggregated items",
                "[8] Show player possessions count",
                " ",
                "[R] Return",
                "[Q] Quit"
            };

            ConsoleKey? key;
            bool running = true;

            while (running)
            {
                Console.Clear();
                _helper.PrintLines(menu);
                key = QuitManager.WaitForKeyOrQuit();

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        ShowItemComplexWithLocation();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        ShowItemComplexWithOwner();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        ShowLoginWithPlayerStats();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        ShowLostItemsSimpleAndComplex();
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        ShowLoginPlayerItems();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        ShowLoginPlayerItems(); // INDIVIDUAL
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        ShowLoginPlayerItemsAggregated(); // AGGREGATED
                        break;
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        ShowPlayerPossessionsCount();
                        break;
                    case ConsoleKey.R:
                        running = false;
                        _helper.Buffer("Returning");
                        break;
                    case ConsoleKey.Q:
                        QuitManager.RequestQuit();
                        break;
                }
            }
        }

        private void ShowItemComplexWithLocation()
        {
            var list = _repo.GetItemComplexWithLocation();
            Console.Clear();
            _helper.BlueText("ItemComplex with Location:\n");

            Console.WriteLine("{0,-30} | {1}", "Lost Item", "Location");
            Console.WriteLine(new string('-', 50));

            foreach (var item in list)
            {
                Console.WriteLine("{0,-30} | {1}", item.LostItem, item.Location ?? "N/A");
            }

            Pause();
        }

        private void ShowItemComplexWithOwner()
        {
            var list = _repo.GetItemComplexWithOwner();
            Console.Clear();
            _helper.BlueText("ItemComplex with Owner:\n");

            Console.WriteLine("{0,-30} | {1}", "Item", "Possession Of");
            Console.WriteLine(new string('-', 50));

            foreach (var item in list)
            {
                Console.WriteLine("{0,-30} | {1}", item.Item, item.PossessionOf ?? "N/A");
            }

            Pause();
        }

        private void ShowLoginWithPlayerStats()
        {
            var list = _repo.GetLoginWithPlayerStats();
            Console.Clear();
            _helper.BlueText("Login with Player Stats:\n");

            Console.WriteLine("{0,-8} | {1,-20} | {2,5} | {3,5}", "LoginID", "Username", "Level", "HP");
            Console.WriteLine(new string('-', 50));

            foreach (var player in list)
            {
                Console.WriteLine("{0,-8} | {1,-20} | {2,5} | {3,5}", player.LoginID, player.Username, player.Level, player.HP);
            }

            Pause();
        }

        private void ShowLostItemsSimpleAndComplex()
        {
            var list = _repo.GetLostItemsSimpleAndComplex();
            Console.Clear();
            _helper.BlueText("Lost Items (Simple + Complex) with Zones:\n");

            Console.WriteLine("{0,-30} | {1,-40} | {2}", "Lost Item", "Description", "Zone");
            Console.WriteLine(new string('-', 90));

            foreach (var item in list)
            {
                Console.WriteLine("{0,-30} | {1,-40} | {2}", item.LostItem, item.Description, item.Zone ?? "N/A");
            }

            Pause();
        }

        private void ShowLoginPlayerItems()
        {
            var list = _repo.GetLoginPlayerItems();
            Console.Clear();
            _helper.BlueText("Login-Player Items (individual rows):\n");

            Console.WriteLine("{0,-8} | {1,-20} | {2,5} | {3,5} | {4}", "LoginID", "Username", "Level", "HP", "Item");
            Console.WriteLine(new string('-', 80));

            foreach (var item in list)
            {
                Console.WriteLine("{0,-8} | {1,-20} | {2,5} | {3,5} | {4}", item.LoginID, item.Username, item.Level, item.HP, item.Item ?? "None");
            }

            Pause();
        }

        private void ShowLoginPlayerItemsAggregated()
        {
            var list = _repo.GetLoginPlayerItemsAggregated();
            Console.Clear();
            _helper.BlueText("Login-Player Items (aggregated):\n");

            Console.WriteLine("{0,-8} | {1,-20} | {2,5} | {3,5} | {4}", "LoginID", "Username", "Level", "HP", "Items");
            Console.WriteLine(new string('-', 90));

            foreach (var item in list)
            {
                Console.WriteLine("{0,-8} | {1,-20} | {2,5} | {3,5} | {4}", item.LoginID, item.Username, item.Level, item.HP, item.Items ?? "None");
            }

            Pause();
        }

        private void ShowPlayerPossessionsCount()
        {
            var list = _repo.GetPlayerPossessionsCount();
            Console.Clear();
            _helper.BlueText("Player Possessions Count:\n");

            Console.WriteLine("{0,-20} | {1}", "Username", "Possessions");
            Console.WriteLine(new string('-', 40));

            foreach (var p in list)
            {
                Console.WriteLine("{0,-20} | {1}", p.Username, p.Possessions);
            }

            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey(true);
        }
    }
}