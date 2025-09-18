using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmollGameDB.Services
{
    public static class QuitManager
    {
        //holder styr på quit-status
        private static bool quitRequested = false;
        //get: public readonly property, kan læses af hele programmet
        public static bool QuitRequested => quitRequested;

        /// <summary>
        ///metode til at bede om at lukke program
        /// </summary>
        public static void RequestQuit()
        {
            if (!quitRequested) //hvis quit ikke er requested allerede
            {
                quitRequested = true; //sæt til true (request quit)
                Buffer();//metodekald til luk ned-skærm
            }
        }
        public static ConsoleKey? WaitForKeyOrQuit()
        {
            while (true)//uendelig løkke
            {
                if (QuitRequested)// hvis quit requested, returner Q
                { return ConsoleKey.Q; }

                if (Console.KeyAvailable) //hvis der er et tastetryk
                {
                    var key = Console.ReadKey(true).Key; //intercept tastetryk

                    if (key == ConsoleKey.Q)
                    {
                        RequestQuit();
                        return ConsoleKey.Q;
                    }
                    else
                    {
                        return key;
                    }
                }

                Thread.Sleep(100);
            }
        }
        public static void Buffer()
        {
            string message = "Quitting";
            int midX = (Console.BufferWidth - message.Length) / 2,
                midY = Console.BufferHeight / 2;

            Console.Clear();
            Console.SetCursorPosition(midX, midY);
            Console.Write($"* {message} *");
            Thread.Sleep(500);

            Console.Clear();
            Console.SetCursorPosition(midX - 2, midY);
            Console.Write($"* * {message} * *");
            Thread.Sleep(500);

            Console.Clear();
            Console.SetCursorPosition(midX - 4, midY);
            Console.Write($"* * * {message} * * *");
            Thread.Sleep(500);
            Console.Clear();
        }
    }
}
