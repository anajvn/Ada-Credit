using AdaCredit.UI.Data;
using AdaCredit.UI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.UseCases
{
    public static class ChangeClientData
    {
        public static void Execute(int index)
        {
            Console.WriteLine("\n----- Alterar dados do cliente -----\n");

            System.Console.Write("CPF da conta: ");
            var flag =long.TryParse(Console.ReadLine(), out long document);

            string newData = "";
            Console.WriteLine("\n\n---------- * ----------\n");

            if (flag)
            {
                switch (index)
                {
                    case 1:
                        Console.Write("\nNome atualizado: ");
                        newData = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("\nCPF atualizado (somente números): ");
                        newData = Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("\nTelefone atualizado (formato (XX) XXXXX-XXXX): ");
                        newData = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("\nEndereço atualizado: ");
                        newData = Console.ReadLine();
                        break;
                    //case 5:
                    //    Console.WriteLine("Uma nova conta será gerada.");
                }
            }
            var repository = new ClientRepository();
            var result = repository.ChangeData(document, index, newData);

            string message = "Alteração finalizada com sucesso!";

            if (!result)
                message = "Não foi possível alterar o cadastro. Verifique os dados ou cadastre um cliente novo.";
            
            Console.WriteLine(message);
            Console.ReadKey() ;
        }
    }
}
