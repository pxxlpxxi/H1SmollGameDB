using SmollGameDB.Database;
using SmollGameDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.ConsoleUI
{
    internal static class LoginMenu
    {
        private static LoginRepository _repo = new();
        private static UIHelper _helper = new();
        public static void Run()
        {

            ConsoleKey key;
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Entities\n");

                Console.WriteLine("[1] Login\n" +
                    "[2] Create Login\n" +
                    "[3] Update Login\n" +
                    "[4] Delete Login\n");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1: //bruger-login aka read
                    case ConsoleKey.NumPad1:
                        UserLogin();
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

            //switch(key){}
        }
        private static void UserLogin()
        {
            Console.Clear();
            var credentials = LoginPrompt();
            //bool valid = ValidateUserLogin(credenials.username, credentials.password);
            if (_repo.ValidateUserLogin(credentials.username, credentials.password))
            {
                _helper.Buffer("Logging in");
            }
            else
            {
                _helper.RedText("\nLogin credentials invalid.");
                _helper.Buffer("Returning to menu");
            }
        }
        private static (string username, string password) LoginPrompt()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = _helper.ReadPassword();
        }
        //private static bool ValidateUserLogin(string username, string password)
        //{
        //    bool validCredentials = _repo.ValidateUserLogin();
        //}
        private static void CreateLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();
        }
        private static void UpdateLogin() { }
        private static void DeleteLogin() { }


    }

}

