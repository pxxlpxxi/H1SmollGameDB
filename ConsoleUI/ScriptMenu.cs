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
                "[2] List itemComplex with owner",
                "[3] Show login with player stats",
                "[4] List lost items (simple + complex) with zone",
                "[5] Show login-player with individual items",
                "[6] Show login-player items",
                "[7] Show login-player with aggregated items",
                "[8] Show player possessions count",
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

                }//end of switchcase

            }//end of running

        }
        private void ShowItemComplexWithLocation()
        {
            var list = _repo.GetItemComplexWithLocation();
            Console.Clear();
            _helper.BlueText("ItemComplex with Location:\n");
            Console.WriteLine();

            foreach (var item in list)
            {
                _helper.CyanText($"Item: {item.LostItem}\n");
                Console.WriteLine($"Location: {item.Location}");
                Console.WriteLine();
            }
            Pause();
        }

        private void ShowItemComplexWithOwner()
        {
            var list = _repo.GetItemComplexWithOwner();
            Console.Clear();
            _helper.BlueText("ItemComplex with Owner:\n");
            Console.WriteLine();

            foreach (var item in list)
            {
                _helper.CyanText($"Item: {item.Item}\n");
                Console.WriteLine($"Owner: {item.PossessionOf ?? "N/A"}");
                Console.WriteLine();
            }
            Pause();
        }

        private void ShowLoginWithPlayerStats()
        {
            var list = _repo.GetLoginWithPlayerStats();
            Console.Clear();
            _helper.BlueText("Login with Player Stats:\n");
            Console.WriteLine();

            foreach (var player in list)
            {
                _helper.CyanText($"Username: {player.Username}\n");
                Console.WriteLine(
                    $"LoginID: {player.LoginID}\n" +
                    $"Level: {player.Level}\n" +
                    $"HP: {player.HP}");
                Console.WriteLine();
            }
            Pause();
        }

        private void ShowLostItemsSimpleAndComplex()
        {
            var list = _repo.GetLostItemsSimpleAndComplex();
            Console.Clear();
            _helper.BlueText("Lost Items (Simple + Complex) with Zones:\n");

            Console.WriteLine();

            foreach (var item in list)
            {
                _helper.CyanText($"Item: {item.LostItem}\n");
                Console.WriteLine(
                    $"Description: {item.Description}\n" +
                    $"Zone: {item.Zone ?? "N/A"}");
                Console.WriteLine();
            }
            Pause();
        }

        private void ShowLoginPlayerItems()
        {
            var list = _repo.GetLoginPlayerItems();
            Console.Clear();
            _helper.BlueText("Login-Player Items (individual rows):\n");

            Console.WriteLine();

            foreach (var item in list)
            {
                _helper.CyanText($"Username: {item.Username}\n");
                Console.WriteLine(
                    $"LoginID: {item.LoginID}\n" +
                    $"Level: {item.Level}\n" +
                    $"HP: {item.HP}\n" +
                    $"Item: {item.Item ?? "None"}");
                Console.WriteLine();
            }
            Pause();
        }

        private void ShowLoginPlayerItemsAggregated()
        {
            var list = _repo.GetLoginPlayerItemsAggregated();
            Console.Clear();
            _helper.BlueText("Login-Player Items (aggregated):\n");

            Console.WriteLine();

            foreach (var item in list)
            {
                _helper.CyanText($"Username: {item.Username}\n");
                Console.WriteLine(
                    $"LoginID: {item.LoginID}\n" +
                    $"Level: {item.Level}\n" +
                    $"HP: {item.HP}\n" +
                    $"Items: {item.Items ?? "None"}");
                Console.WriteLine();
            }
            Pause();
        }

        private void ShowPlayerPossessionsCount()
        {
            var list = _repo.GetPlayerPossessionsCount();
            Console.Clear();
            _helper.BlueText("Player Possessions Count:\n");

            Console.WriteLine();

            foreach (var p in list)
            {
                _helper.CyanText($"Username: {p.Username}\n");
                Console.WriteLine($"Possessions: {p.Possessions}");
                Console.WriteLine();
            }
            Pause();
        }

        private void Pause()
        {

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    }
}