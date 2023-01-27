using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaCredit.UI.Data;
using AdaCredit.UI.UseCases;

namespace AdaCredit.UI.Domain
{
    internal static class Login
    {
        public static void Show()
        {
            bool loggedIn = false;

            do
            {
                System.Console.Clear();

                System.Console.Write("Usuário: ");
                string username = Console.ReadLine();

                System.Console.Write("Senha: ");
                var cleanPassword = EnterPassword.Execute();

                if (username == "user" && cleanPassword == "pass" && IsFirstAccess.Execute())
                    break;

                var repository = new EmployeeRepository();
                var result = repository.IsLoginValid(username, cleanPassword);

                if (result)
                {
                    repository.SaveAccess(username);
                    loggedIn = true;
                    break;
                }

                System.Console.WriteLine("\n\nLogin não efetuado. Por favor, verifique as informações.");
                System.Console.ReadKey();

            } while (!loggedIn);

            System.Console.Clear();
            System.Console.WriteLine("Login efetuado com sucesso.");
            System.Console.ReadKey();
            System.Console.Clear();
        }
    }
}
