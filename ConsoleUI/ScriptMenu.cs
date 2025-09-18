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
    internal class ScriptMenu
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
                "[2] List itemComplex with location",
                "[3] Show login with player stats",
                "[4] List lost items (simple + complex) with zone",
                "[5] Show login-player with individual items",
                "[6] Show login-player with aggregated items",
                "[7] Show login-player with aggregated items",
                " ",
                "[R] Return",
                "[Q] Quit" };

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
                        ShowLoginPlayerItems();
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        ShowPlayerPossessionsCount();
                        break;
                    case ConsoleKey.R:
                        running = false;
                        _helper.Buffer("Returning");
                        break;
                    case ConsoleKey.Q:
                        QuitManager.RequestQuit();
                        break;

                }//end of switchcase

            }//end of running

        }
        private void ShowItemComplexWithLocation()
        {
            var list = _repo.GetItemComplexWithLocation();
            Console.WriteLine("\nItemComplex with Location:");
            foreach (var item in list)
            {
                Console.WriteLine($"Item: {item.LostItem}, Location: {item.Location}");
            }
            Pause();
        }

        private void ShowItemComplexWithOwner()
        {
            var list = _repo.GetItemComplexWithOwner();
            Console.WriteLine("\nItemComplex with Owner:");
            foreach (var item in list)
            {
                Console.WriteLine($"Item: {item.Item}, Owner: {item.PossessionOf}");
            }
            Pause();
        }

        private void ShowLoginWithPlayerStats()
        {
            var list = _repo.GetLoginWithPlayerStats();
            Console.WriteLine("\nLogin with Player Stats:");
            foreach (var player in list)
            {
                Console.WriteLine($"LoginID: {player.LoginID}, Username: {player.Username}, Level: {player.Level}, HP: {player.HP}");
            }
            Pause();
        }

        private void ShowLostItemsSimpleAndComplex()
        {
            var list = _repo.GetLostItemsSimpleAndComplex();
            Console.WriteLine("\nLost Items (Simple + Complex) with Zones:");
            foreach (var item in list)
            {
                Console.WriteLine($"Item: {item.LostItem}, Description: {item.Description}, Zone: {item.Zone ?? "N/A"}");
            }
            Pause();
        }

        private void ShowLoginPlayerItems()
        {
            var list = _repo.GetLoginPlayerItems();
            Console.WriteLine("\nLogin-Player Items (individual rows):");
            foreach (var item in list)
            {
                Console.WriteLine($"LoginID: {item.LoginID}, Username: {item.Username}, Level: {item.Level}, HP: {item.HP}, Item: {item.Item ?? "None"}");
            }
            Pause();
        }

        private void ShowLoginPlayerItemsAggregated()
        {
            var list = _repo.GetLoginPlayerItemsAggregated();
            Console.WriteLine("\nLogin-Player Items (aggregated):");
            foreach (var item in list)
            {
                Console.WriteLine($"LoginID: {item.LoginID}, Username: {item.Username}, Level: {item.Level}, HP: {item.HP}, Items: {item.Items ?? "None"}");
            }
            Pause();
        }

        private void ShowPlayerPossessionsCount()
        {
            var list = _repo.GetPlayerPossessionsCount();
            Console.WriteLine("\nPlayer Possessions Count:");
            foreach (var p in list)
            {
                Console.WriteLine($"Username: {p.Username}, Possessions: {p.Possessions}");
            }
            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    } }