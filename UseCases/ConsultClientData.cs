using AdaCredit.UI.Data;
using AdaCredit.UI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.UseCases
{
    public static class ConsultClientData
    {
        public static void Execute(int index)
        {
            Console.WriteLine("\n----- Consultar dados do cliente -----\n");
            
            string info="";
            string secondInfo="";

            switch (index)
            {
                case 1:
                    Console.Write("Nome completo: ");
                    info = Console.ReadLine();
                    break;
                case 2:
                    Console.Write("CPF (somente números): ");
                    info = Console.ReadLine();
                    break;
                case 3:
                    Console.Write("Número da conta: ");
                    info = Console.ReadLine();

                    Console.Write("Número da agência: ");
                    secondInfo = Console.ReadLine();
                    break;
            }
            
            Console.WriteLine("\n---------- * ----------\n");
            var repository = new ClientRepository();
            var result = repository.GetInfos(index, info, secondInfo);

            if(!result)
                Console.WriteLine("Não foi possível encontrar o cadastro. Verifique os dados ou cadastre um cliente novo.");

            Console.ReadKey();
        }
    }
}
