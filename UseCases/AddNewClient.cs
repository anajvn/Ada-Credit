using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaCredit.UI.Data;
using AdaCredit.UI.Domain;

namespace AdaCredit.UI.UseCases
{
    public static class AddNewClient
    {
        public static void Execute()
        {
            Console.WriteLine("\n----- Adicionar novo cliente -----\n");

            System.Console.Write("Nome: ");
            string name = Console.ReadLine();
            
            System.Console.Write("CPF (somente números): ");
            long document = long.Parse(Console.ReadLine());

            System.Console.Write("Telefone (XX) XXXXX-XXXX: ");
            string phone = Console.ReadLine();

            System.Console.Write("Endereço: ");
            string address = Console.ReadLine();

            var client = new Client(name, document, phone, address);

            Console.WriteLine("\n---------- * ----------\n");

            var repository = new ClientRepository();
            var result = repository.AddClient(client);

            if (result)
                Console.WriteLine("\nCliente adicionado com sucesso!");
            else
                Console.WriteLine("\nFalha ao cadastrar novo cliente.");

            Console.ReadKey();
        }
    }
}
