using AdaCredit.UI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.UseCases
{
    public class CheckFailedTransactions
    {
        public static void Execute()
        {
            Console.WriteLine("\n----- Transações com erro -----\n");

            TransactionsRepository.FailedTransactions();

            Console.ReadKey();
        }
    }
}
