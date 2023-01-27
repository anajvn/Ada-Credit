using AdaCredit.UI.Data;
using AdaCredit.UI.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.UseCases
{
    public class IsFirstAccess
    {
        public static bool Execute()
        {
            var repository = new EmployeeRepository();
            var result = repository.FirstAccess();

            if (!result)
                return false;

            Console.WriteLine(
                "\n\nEste é o primeiro acesso. É necessário criar um novo funcionário para prosseguir.\nEstamos redirecionando a página para isso...");
            Console.ReadKey();
            Console.Clear();

            AddNewEmployee.Execute();
            CreateEmployees.Execute();


            return true;
        }
    }
}
