using SmollGameDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.ConsoleUI
{
    public static class Menu
    {
        private static UIHelper _helper = new();
        public static void Run()
        {



            ConsoleKey key;
            bool running = true;

            while (running)
            {
                string[] menu = { "Entitiy Management System", " ",
                        "[1] Login",
                        " ",
                        "Administrate Entities:",
                        "[2] Simple Item",
                        "[3] Complex Item",
                        "[4] Location",
                        "[5] Monster",
                        "[6] Player",
                        " " ,
                        "[7] From script",
                        " ",
                        "[Q] Quit"};

                _helper.PrintLines(menu);
                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1: //itemsmp
                    case ConsoleKey.NumPad1:
                        LoginMenu login = new();
                        login.Run();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        ItemSmpMenu itemS = new();
                        itemS.Run();

                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        ItemCpxMenu itemC = new();
                        itemC.Run();

                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        LocationMenu location = new();
                        location.Run();

                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        MonsterTypeMenu monsterT = new();
                        monsterT.Run();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        PlayerMenu player = new();
                        player.Run();
                        break;
                        case ConsoleKey.D7:
                            case ConsoleKey.NumPad7:
                        ScriptMenu script = new();
                        script.Run();
                        break;
                    case ConsoleKey.Q:
                        running = false;
                        break;

                }//end of switchcase
            }//end of running
        }
    }
}
