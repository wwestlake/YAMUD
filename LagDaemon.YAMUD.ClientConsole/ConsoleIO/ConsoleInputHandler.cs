using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.ClientConsole.ConsoleIO
{
    internal class ConsoleInputHandler
    {
        public string GetInput(string prompt)
        {
            Console.Write(prompt);
            Console.Out.Flush();
            return Console.ReadLine();
        }

        public string GetSecretInput(string prompt)
        {
            Console.Write(prompt);
            Console.Out.Flush();
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password.Remove(password.Length - 1, 1);
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    Console.Write("*");
                    password.Append(key.KeyChar);
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password.ToString();
        }
    }
}
