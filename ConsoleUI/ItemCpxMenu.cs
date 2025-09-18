using SmollGameDB.Models;
using SmollGameDB.Repositories;
using SmollGameDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.ConsoleUI
{
    internal class ItemCpxMenu
    {
        private readonly ItemCpxRepository _repo = new();
        private readonly UIHelper _helper = new();
        public void Run()
        {
            //menu list
            string[] menu = {
                "Administrate Complex Items",
                " ",
                "[1] Create",
                "[2] Read",
                "[3] Update",
                "[4] Delete",
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
                        Create();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Read(); //bruger-login aka read
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Update();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Delete();
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
        private void Create()
        {
            Console.Clear();
            Console.WriteLine("Create new complex item");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Type: ");
            string type = Console.ReadLine();

            Console.Write("Description (optional): ");
            string description = Console.ReadLine();

            Console.Write("Player ID (can be empty): ");
            string playerInput = Console.ReadLine();
            int? playerId = string.IsNullOrWhiteSpace(playerInput) ? null : int.Parse(playerInput);

            Console.Write("Location ID (can be empty): ");
            string locationInput = Console.ReadLine();
            int? locationId = string.IsNullOrWhiteSpace(locationInput) ? null : int.Parse(locationInput);

            ItemCpx item = new()
            {
                Name = name,
                Type = type,
                Description = description,
                PlayerId = playerId ?? 0,
                LocationId = locationId ?? 0
            };

            _repo.CreateItem(item);
            _helper.GreenText("Item created.");
            Console.ReadKey();
            _helper.Buffer("Returning");
        }

        private void Read()
        {
            Console.Clear();
            Console.WriteLine("All complex items:\n");

            List<ItemCpx> items = _repo.GetAllItems();

            foreach (ItemCpx item in items)
            {
                _helper.CyanText(item.Name + "\n");
                Console.WriteLine(
                    $"Type: {item.Type}\n" +
                    $"Description: {item.Description}\n" +
                    $"ID: {item.Id}\n" +
                    $"PlayerID: {item.PlayerId}\n" +
                    $"LocationID: {item.LocationId}");
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private void Update()
        {
            Console.Clear();
            Console.WriteLine("Update complex item");

            Console.Write("Item ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("New Name: ");
            string name = Console.ReadLine();

            Console.Write("New Type: ");
            string type = Console.ReadLine();

            Console.Write("New Description: ");
            string description = Console.ReadLine();

            Console.Write("New Player ID (or leave empty): ");
            string playerInput = Console.ReadLine();
            int? playerId = string.IsNullOrWhiteSpace(playerInput) ? null : int.Parse(playerInput);

            Console.Write("New Location ID (or leave empty): ");
            string locationInput = Console.ReadLine();
            int? locationId = string.IsNullOrWhiteSpace(locationInput) ? null : int.Parse(locationInput);

            ItemCpx item = new()
            {
                Id = id,
                Name = name,
                Type = type,
                Description = description,
                PlayerId = playerId ?? 0,
                LocationId = locationId ?? 0
            };

            if (_repo.UpdateItem(item))
            {
                _helper.GreenText("Item updated.");
            }
            Console.ReadKey();
            _helper.Buffer("Returning");
        }

        private void Delete()
        {
            Console.Clear();
            Console.WriteLine("Delete complex item");

            Console.Write("Item ID: ");
            int id = int.Parse(Console.ReadLine());

            (bool exists, bool success) del = _repo.DeleteItem(id);

            if (!del.exists)
            {
                _helper.RedText("Item does not exist");
            }
            else if (!del.success)
            {
                _helper.RedText("Delete failed");
            }
            else
            {
                _helper.GreenText("Item deleted");
            }

            Console.ReadKey();
            _helper.Buffer("Returning");
        }
    }
}
