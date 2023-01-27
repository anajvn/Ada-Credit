using AdaCredit.UI.Domain;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;
using AdaCredit.UI.UseCases;

namespace AdaCredit.UI.Data
{
    internal class UserRepository
    {
        private static List<User> _users = new List<User>();
        
        public static string GenerateSalt()
        {
            return new Faker().Random.Replace("$2a$10$***********************");
        }

        public static string GenerateHash(string cleanPassword, out string salt)
        {
            salt = GenerateSalt();
            var hashedPassword = HashPassword(cleanPassword, salt, false);

            return hashedPassword;
        }

        public static string Hash(string cleanPassword, string salt)
        {
            var hashedPassword = HashPassword(cleanPassword, salt, false);

            return hashedPassword;
        }

        public static string ChangeUsername(List<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                _users.Add(employee.User);
            }
            bool flag;
            string username;
            do
            {
                Console.Write("\nNovo usuário: ");
                username = Console.ReadLine();
                flag = _users.Any(e => e.Username == username);
                if (!flag)
                    break;
                Console.Write("Usuário indisponível. Tente outro nome.");
            } while (flag);

            return username;
        }
        public static string ChangePassword(out string salt)
        {
            var flag = false;
            string hashedPassword = "";
            salt = "";
            do
            {
                Console.Write("\nNova senha: ");
                var firstTry = EnterPassword.Execute();

                Console.Write("\nDigite novamente a nova senha: ");
                var secondTry = EnterPassword.Execute();

                flag = firstTry == secondTry;

                if (!flag)
                {
                    Console.WriteLine("\nSenhas não coincidem. Tente novamente.\n");
                    continue;
                }

                hashedPassword = GenerateHash(firstTry, out salt);

            } while (!flag);

            return hashedPassword;
        }
    }
}
