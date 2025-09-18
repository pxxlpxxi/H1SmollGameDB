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
        private void Create() { }
        private void Read() { }
        private void Update() { }
        private void Delete() { }
    }
}

