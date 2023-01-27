using AdaCredit.UI.Data;
using AdaCredit.UI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.UseCases
{
    public class CheckActiveClients
    {
        public static void Execute()
        {
            Console.WriteLine("\n----- Clientes ativos e saldos -----\n");

            var repository = new ClientRepository();
            repository.CheckClientsAndBalances();
            
            Console.ReadKey();
        }
    }
}
