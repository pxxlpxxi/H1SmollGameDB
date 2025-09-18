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
    internal class LocationMenu
    {
            private readonly LocationRepository _repo = new();
            private readonly UIHelper _helper = new();
        public void Run()
        {
            //menu list
            string[] menu = {
                "Administrate Locations",
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
            Console.WriteLine("Create new location");

            Console.Write("Zone: ");
            string zone = Console.ReadLine();

            Console.Write("Description (optional): ");
            string description = Console.ReadLine();

            Location location = new()
            {
                Zone = zone,
                Description = description
            };

            _repo.CreateLocation(location);
            _helper.GreenText("Location created.");
            Console.ReadKey();
            _helper.Buffer("Returning");
        }

        private void Read()
        {
            Console.Clear();
            Console.WriteLine("All locations:\n");

            List<Location> locations = _repo.GetAllLocations();

            foreach (Location loc in locations)
            {
                _helper.BlueText(loc.Zone + "\n");
                Console.WriteLine(
                    $"Description: {loc.Description}\n" +
                    $"ID: {loc.Id}");
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private void Update()
        {
            Console.Clear();
            Console.WriteLine("Update location");

            Console.Write("Location ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("New Zone: ");
            string zone = Console.ReadLine();

            Console.Write("New Description: ");
            string description = Console.ReadLine();

            Location location = new()
            {
                Id = id,
                Zone = zone,
                Description = description
            };

            if (_repo.UpdateLocation(location))
            {
                _helper.GreenText("Location updated.");
            }
            Console.ReadKey();
            _helper.Buffer("Returning");
        }

        private void Delete()
        {
            Console.Clear();
            Console.WriteLine("Delete location");

            Console.Write("Location ID: ");
            int id = int.Parse(Console.ReadLine());

            if (_repo.DeleteLocation(id))
            {
                _helper.GreenText("Location deleted.");
            }
            else
            {
                _helper.RedText("Delete failed. Location may not exist.");
            }

            Console.ReadKey();
            _helper.Buffer("Returning");
        }
    }
}