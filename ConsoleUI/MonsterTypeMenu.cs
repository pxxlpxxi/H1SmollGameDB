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
    internal class MonsterTypeMenu
    {
        private readonly MonsterTypeRepository _repo = new();
        private readonly UIHelper _helper = new();
        public void Run()
        {
            //menu list
            string[] menu = {
                "Administrate Monster Types",
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
            Console.WriteLine("Create new monster type");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            MonsterType monster = new()
            {
                Name = name
            };

            _repo.Create(monster);
            _helper.GreenText("Monster type created.");
            Console.ReadKey();
            _helper.Buffer("Returning");
        }

        private void Read()
        {
            Console.Clear();
            Console.WriteLine("All monster types:\n");

            List<MonsterType> monsters = _repo.GetAll();

            foreach (MonsterType monster in monsters)
            {
                _helper.CyanText(monster.Name + "\n");
                Console.WriteLine($"ID: {monster.Id}\n");
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private void Update()
        {
            Console.Clear();
            Console.WriteLine("Update monster type");

            Console.Write("Monster Type ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("New Name: ");
            string name = Console.ReadLine();

            MonsterType monster = new()
            {
                Id = id,
                Name = name
            };

            if (_repo.Update(monster))
            {
                _helper.GreenText("Monster type updated.");
            }
            Console.ReadKey();
            _helper.Buffer("Returning");
        }

        private void Delete()
        {
            Console.Clear();
            Console.WriteLine("Delete monster type");

            Console.Write("Monster Type ID: ");
            int id = int.Parse(Console.ReadLine());

            if (_repo.Delete(id))
            {
                _helper.GreenText("Monster type deleted.");
            }
            else
            {
                _helper.RedText("Delete failed. Monster type may not exist.");
            }

            Console.ReadKey();
            _helper.Buffer("Returning");
        }
    }
}