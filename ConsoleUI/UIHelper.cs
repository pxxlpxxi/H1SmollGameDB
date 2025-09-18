using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.ConsoleUI
{
    internal class UIHelper
    {
        public void PrintLines(string[] lines)
        {
            foreach (string line in lines)
            {
                Console.WriteLine(line);
                           }
        }
        public string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do //while not Enter
            {
                key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    //removes one character from password
                    password = password.Substring(0, password.Length - 1);
                    //updates display: moves back, overwrites with space, moves back again
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar; //adds one character to password
                    Console.Write("*"); //updates display with asterisk
                }
            }
            while (key.Key != ConsoleKey.Enter);

            return password;
        }
        public void Buffer(string text)
        {
            Console.Clear();
            Console.SetCursorPosition(11, 2);
            Console.Write($"* {text} *");
            Thread.Sleep(500);

            Console.Clear();
            Console.SetCursorPosition(9, 2);
            Console.Write($"* * {text} * *");
            Thread.Sleep(500);

            Console.Clear();
            Console.SetCursorPosition(7, 2);
            Console.Write($"* * * {text} * * *");
            Thread.Sleep(500);
            Console.Clear();
        }
        public void RedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(text);
            Console.ResetColor();
        }
        public void GreenText(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(text);
            Console.ResetColor();
        }
        public void BlueText(string text) {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
