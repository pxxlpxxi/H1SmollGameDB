using SmollGameDB.Database;
using SmollGameDB.Repositories;
using SmollGameDB.Services;
using SmollGameDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.ConsoleUI
{
    internal class LoginMenu
    {
        private readonly LoginRepository _repo = new();
        private readonly UIHelper _helper = new();
        public void Run()
        {

            //Console.WriteLine("Entities");
            string[] menu = {
                "Administrate Logins",
                " ",
                "[1] Create",
                "[2] Login",
                "[3] Update Login",
                "[4] Delete Login",
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
                        UserLogin(); //bruger-login aka read
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

            //switch(key){}
        }
        private void UserLogin()
        {
            Console.Clear();
            Console.WriteLine("User login\n");

            var credentials = LoginPrompt();
            //bool valid = ValidateUserLogin(credenials.username, credentials.password);
            if (_repo.ValidateUserLogin(credentials.username, credentials.password) != null)
            {
                _helper.Buffer("Logging in");
            }
            else
            {
                _helper.RedText("\nLogin credentials invalid.");
                Thread.Sleep(2000);
                _helper.Buffer("Returning to menu");
            }
        }
        private (string username, string password) LoginPrompt()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = _helper.ReadPassword();
            return (username, password);
        }
        private void Create()
        {
            bool creatingLogin = true;

            while (creatingLogin)
            {
                Console.Clear();
                Console.WriteLine("Create login\n");

                string username = PromptUsername("Username: ");
                //string username = Console.ReadLine();
                if (!string.IsNullOrEmpty(username))
                {
                    Console.Write("Password: ");
                    string password = Console.ReadLine();
                    if (_repo.CreateLogin(username, password))
                    {
                        _helper.GreenText("Login created");
                        Thread.Sleep(2000);
                        _helper.Buffer("Returning to menu");
                        creatingLogin = false;
                    }
                    else
                    {
                        _helper.RedText("Something went wrong");
                        creatingLogin = QuitCheckWithMessage("try again");
                    }
                }
                else
                {
                    _helper.RedText("Username cannot be empty");
                    creatingLogin = QuitCheckWithMessage("try another username");
                }
            } //end of while
        }
        private string PromptUsername(string prompt)
        {
            bool usernameUnique = false;
            string username = "";

            int i = 0;


            while (!usernameUnique && i < 5)
            {

                var pos = Console.GetCursorPosition();
                Console.Write(prompt);
                int input = Console.GetCursorPosition().Left;
                username = Console.ReadLine();

                if (!_repo.UsernameExists(username))
                {
                    usernameUnique = true;
                    return username;
                }
                else
                {
                    Console.SetCursorPosition(input, pos.Top);
                    _helper.RedText("Username not available");
                    Thread.Sleep(1500);
                    int length = username.Length + 25;
                    Console.SetCursorPosition(pos.Left, pos.Top);
                    string correction = new string(' ', length);
                    Console.Write(prompt);
                    Console.Write(correction);
                    Console.SetCursorPosition(pos.Left, pos.Top);
                    //Console.SetCursorPosition(pos.Left, pos.Top);
                }
                i++;
            }
            return null;
        }

        private bool QuitCheckWithMessage(string alternativeActionToQuit)
        {
            Console.WriteLine($"\nPress [Q] to go back or any other key to {alternativeActionToQuit}.");
            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.Q)
            {
                return false;
            }
            return true;
        }
        private void Update()
        {
            bool searching = true;
            while (searching)
            {
                Console.Clear();
                Console.WriteLine("Update login\n");
                Console.Write("Username: ");
                string username = Console.ReadLine();

                Login login = FindLogin(username);

                if (login != null)
                {
                    UpdateLogin(login);
                    searching = false;
                }
                else
                {
                    _helper.RedText("Login not found");
                    searching = QuitCheckWithMessage("try again");
                }
            }//end of searching
            _helper.Buffer("Returning to menu");
        }
        private void UpdateLogin(Login login)
        {
            bool update = true;

            while (update)
            {
                Console.Clear();
                Console.Write("Update credentials for '");
                _helper.CyanText(login.Username);
                Console.WriteLine("'.");

                bool confirm = QuitCheckWithMessage("update login");

                if (!confirm)
                {
                    update = false;
                }
                else
                {
                    Console.WriteLine("\nLeave fields blank or empty to keep current username or password.");
                    string prompt = "New username: ";
                    string newUsername = PromptUsername(prompt);//"New username (leave blank to keep current username): "
                                                                //Console.WriteLine("New username (leave blank to keep current username): ");
                                                                //string newUsername = Console.ReadLine();
                                                                //if (_repo.UsernameExists(newUsername)) { break; }
                    if (string.IsNullOrEmpty(newUsername))
                    {
                        newUsername = login.Username;
                    }
                    _helper.CyanText("Username set: "+newUsername);

                    Console.Write("\nNew password (leave blank to keep current): ");
                    string newPassword = Console.ReadLine();

                    if (_repo.UpdateLogin(login.Username, newUsername, newPassword))
                    {
                        _helper.GreenText("Login updated.");
                        Thread.Sleep(2000);
                        update = false;
                    }
                    else
                    {
                        _helper.RedText("Failed to update login.");
                        QuitCheckWithMessage("try again");
                    }
                }
            }
        }
        private Login FindLogin(string username)
        {
            if (_repo.UsernameExists(username))
            {
                return new Login { Username = username };
            }
            else
            {
                _helper.RedText("Username not found.");
                Thread.Sleep(2000);
                return null;
            }
        }
        private void DeleteLogin(Login login)
        {

            bool deleting = true;

            while (deleting)
            {
                Console.Write("Deleting login '");
                _helper.CyanText(login.Username);
                Console.WriteLine("'");
                bool confirm = QuitCheckWithMessage("delete login");
                if (!confirm)
                {
                    deleting = false;
                }
                else
                {
                    if (_repo.DeleteLogin(login.Username))
                    {
                        _helper.GreenText("Login deleted");
                        Thread.Sleep(2000);
                        deleting = false;
                    }
                    else
                    {
                        _helper.RedText("Failed to update login.");
                        QuitCheckWithMessage("try again");
                    }
                }

            }
        }


        private void Delete()
        {
            bool searching = true;

            while (searching)
            {
                Console.Clear();
                Console.WriteLine("Delete login\n");

                Console.Write("Username: ");
                string username = Console.ReadLine();

                Login login = FindLogin(username);

                if (login != null)
                {
                    DeleteLogin(login);
                    searching = false;
                }
                else
                {
                    _helper.RedText("Login not found");
                    searching = QuitCheckWithMessage("try again");
                }
            }//end of searching

            _helper.Buffer("Returning to menu");
        }
    }
}



