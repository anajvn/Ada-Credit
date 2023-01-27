using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.UseCases
{
    public static class EnterPassword
    {
        public static string Execute()
        {
            var cleanPassword = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && cleanPassword.Length > 0)
                {
                    Console.Write("\b \b");
                    cleanPassword = cleanPassword[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    cleanPassword += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            return cleanPassword;
        }
    }
}
