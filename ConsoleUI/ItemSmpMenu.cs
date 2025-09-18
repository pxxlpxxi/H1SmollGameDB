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
    internal class ItemSmpMenu
    {
        private readonly ItemSmpRepository _repo = new();
        private readonly UIHelper _helper = new();
        public void Run()
        {
            //menu list
            string[] menu = {
                "Administrate Simple Items",
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
            Console.WriteLine("Create new simple item");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Description (optional): ");
            string description = Console.ReadLine();

            Console.Write("Player ID (optional): ");
            string playerInput = Console.ReadLine();
            int? playerId = string.IsNullOrWhiteSpace(playerInput) ? null : int.Parse(playerInput);

            ItemSmp item = new()
            {
                Name = name,
                Description = description,
                PlayerId = playerId ?? 0
            };

            _repo.CreateItem(item);
            _helper.GreenText("Item created.");
            Console.ReadKey();
            _helper.Buffer("Returning");
        }
        private void Read()
        {
            Console.Clear();
            Console.WriteLine("All simple items:\n");

            List<ItemSmp> items = _repo.GetAllItems();

            foreach (ItemSmp item in items)
            {
                _helper.BlueText(item.Name + "\n");
                Console.WriteLine(
                    $"Description: {item.Description}\n" +
                    $"ID: {item.Id}\n" +
                    $"PlayerID: {item.PlayerId}");
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private void Update()
        {
            Console.Clear();
            Console.WriteLine("Update simple item");

            Console.Write("Item ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("New Name: ");
            string name = Console.ReadLine();

            Console.Write("New Description: ");
            string description = Console.ReadLine();

            Console.Write("New Player ID (optional): ");
            string playerInput = Console.ReadLine();
            int? playerId = string.IsNullOrWhiteSpace(playerInput) ? null : int.Parse(playerInput);

            ItemSmp item = new()
            {
                Id = id,
                Name = name,
                Description = description,
                PlayerId = playerId ?? 0
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
            Console.WriteLine("Delete simple item");

            Console.Write("Item ID: ");
            int id = int.Parse(Console.ReadLine());

            if (_repo.DeleteItem(id))
            {
                _helper.GreenText("Item deleted.");
            }
            else
            {
                _helper.RedText("Delete failed. Item may not exist.");
            }

            Console.ReadKey();
            _helper.Buffer("Returning");
        }
    }
}