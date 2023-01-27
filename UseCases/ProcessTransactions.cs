using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaCredit.UI.Data;

namespace AdaCredit.UI.UseCases
{
    public class ProcessTransactions
    {
        public static void Execute()
        {
            Console.WriteLine("\n----- Processando transações...\n");

            var repository = new TransactionsRepository();
            var result = repository.Process();

            string message = "\nTransações processadas com sucesso!";

            if (!result)
                message = "\nNão existem transações a serem processadas.";

            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
