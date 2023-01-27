using AdaCredit.UI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.UseCases
{
    public class CheckInactiveClients
    {
        public static void Execute()
        {
            Console.WriteLine("\n----- Clientes inativos -----\n");

            var repository = new ClientRepository();
            repository.CheckInactiveClients();

            Console.ReadKey();
        }
    }
}
