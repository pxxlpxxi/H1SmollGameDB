using SmollGameDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.ConsoleUI
{
    internal static class Menu
    {
        public static void Run()
        {
            ConsoleKey key;
            bool running = true;


            while (running)
            {
                Console.WriteLine("Entities\n");

                Console.WriteLine("[1] Simple Item\n" +
                    "[2] Complex Item\n" +
                    "[3] Location\n" +
                    "[4] Login\n" +
                    "[5] Monster\n");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1: //itemsmp
                    case ConsoleKey.NumPad1:
                        ItemSmpMenu.Run();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        ItemCpxMenu.Run();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        LocationMenu.Run();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        LoginMenu.Run();
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        MonsterTypeMenu.Run();
                        break;
                    case ConsoleKey.Q:
                        running = false;
                        break;

                }//end of switchcase
            }//end of running
        }
    }
}
