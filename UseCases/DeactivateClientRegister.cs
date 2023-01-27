using AdaCredit.UI.Data;
using AdaCredit.UI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.UseCases
{
    public static class DeactivateClientRegister
    {
        public static void Execute()
        {
            Console.WriteLine("\n----- Desativar registro de cliente -----\n");

            System.Console.Write("CPF (somente números): ");
            long document = long.Parse(Console.ReadLine());

            Console.WriteLine("\n\n---------- * ----------\n");

            var repository = new ClientRepository();
            var result = repository.DeactivateClient(document);

            string message = "\nOperação realizada com sucesso!";

            if (!result)
                message = "\nNão foi possível realizar a operação.";

            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
