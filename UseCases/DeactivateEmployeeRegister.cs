using AdaCredit.UI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.UseCases
{
    public class DeactivateEmployeeRegister
    {
        public static void Execute()
        {
            Console.WriteLine("\n----- Desativar registro de funcionário -----\n");

            System.Console.Write("CPF (somente números): ");
            long.TryParse(Console.ReadLine(), out long document);

            Console.WriteLine("\n---------- * ----------\n");

            var repository = new EmployeeRepository();
            var result = repository.DeactivateEmployee(document);

            string message = "Operação realizada com sucesso!";

            if (!result)
                message = "Não foi possível realizar a operação.";

            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
