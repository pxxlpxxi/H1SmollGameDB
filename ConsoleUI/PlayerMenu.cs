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
    internal class PlayerMenu
    {
        private static PlayerRepository _repo = new();
        private static UIHelper _helper = new();
        public void Run()
        {
            //menu list
            string[] menu = {
                "Administrate Players",
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
        private void Create() {
            Console.Clear();
            Console.WriteLine("Create new player");


            Console.Write("Login ID: ");
            int loginId = int.Parse(Console.ReadLine());

            Console.Write("Level: ");
            int level = int.Parse(Console.ReadLine());

            Console.Write("HP: ");
            int hp = int.Parse(Console.ReadLine());

            Player player = new(){ LoginID = loginId, Level = level, HP = hp };
            _repo.CreatePlayer(player);

            _helper.GreenText("Player created.");
            Console.ReadKey();
            _helper.Buffer("Returning");
        }
        private void Read() {
            Console.Clear();
            Console.WriteLine("All players:\n");
            
            List<Player> players = _repo.GetAllPlayers();

            foreach (Player p in players)
            {
                Console.WriteLine($"ID: {p.ID}, LoginID: {p.LoginID}, Level: {p.Level}, HP: {p.HP}");
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        private void Update() { }
        private void Delete() { }
    }
}

