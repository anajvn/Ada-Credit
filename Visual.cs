using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI
{
    public class Visual
    {
        public static void Apply()
        { 
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void Intro()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine("\n\n\n\n\n\n");
            var padLeftImage = (Console.WindowWidth / 2) + 35;
            Console.WriteLine("                 _              _____                     _   _   _   ".PadLeft(padLeftImage));
            Console.WriteLine("     /\\         | |            / ____|                   | | (_) | |  ".PadLeft(padLeftImage));
            Console.WriteLine("    /  \\      __| |   __ _    | |       _ __    ___    __| |  _  | |_ ".PadLeft(padLeftImage));
            Console.WriteLine("   / /\\ \\    / _` |  / _` |   | |      | '__|  / _ \\  / _` | | | | __|".PadLeft(padLeftImage));
            Console.WriteLine("  / ____ \\  | (_| | | (_| |   | |____  | |    |  __/ | (_| | | | | |_ ".PadLeft(padLeftImage));
            Console.WriteLine(" /_/    \\_\\  \\__,_|  \\__,_|    \\_____| |_|     \\___|  \\__,_| |_|  \\__|".PadLeft(padLeftImage));
            Console.WriteLine("                                                                      ".PadLeft(padLeftImage));
            Console.WriteLine("                                                                      ".PadLeft(padLeftImage));

            string slogan = "APRENDENDO JUNTOS A SER CADA VEZ MAIS!";
            var padLeftSlogan = (Console.WindowWidth / 2) + slogan.Length/2;
            Console.WriteLine(slogan.PadLeft(padLeftSlogan));

            Console.WriteLine("\n\n\n\n");
            string procedure = "Aperte qualquer tecla para entrar no sistema...";
            var padLeftProcedure = (Console.WindowWidth / 2) + procedure.Length/2;
            Console.WriteLine(procedure.PadLeft(padLeftProcedure));
            Console.ReadKey();

            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
