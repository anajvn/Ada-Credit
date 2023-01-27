using AdaCredit.UI.Data;
using AdaCredit.UI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.UseCases
{
    public static class AddNewEmployee
    {
        public static void Execute()
        {
            Console.WriteLine("\n----- Adicionar novo funcionário -----\n");

            Console.Write("Nome: ");
            string name = Console.ReadLine();

            Console.Write("\nCPF (somente números): ");
            bool documentFlag  = long.TryParse(Console.ReadLine(), out long document);

            var result = false;
            if (documentFlag)
            {
                var employee = new Employee(name, document);
                var repository = new EmployeeRepository();

                result = repository.AddEmployee(employee);
            }

            Console.WriteLine("\n\n---------- * ----------\n");

            if (result)
            {
                Console.WriteLine("\n\nFuncionário adicionado com sucesso!");
            }
            else
            {
                Console.WriteLine("\n\nFalha ao cadastrar novo funcionário.");
            }
            Console.ReadKey();
        }
    }
}
