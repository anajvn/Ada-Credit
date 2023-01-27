using AdaCredit.UI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.UseCases
{
    public class CheckActiveEmployees
    {
        public static void Execute()
        {
            Console.WriteLine("\n----- Funcionários ativos e último login-----\n");

            var repository = new EmployeeRepository();
            repository.CheckEmployeesAndLogin();

            Console.ReadKey();
        }
    }
}
