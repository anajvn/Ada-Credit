using AdaCredit.UI.Domain;
using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;
using Bogus.DataSets;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using AdaCredit.UI.UseCases;
using System.Xml.Linq;
using static System.Environment;

namespace AdaCredit.UI.Data
{
    public class EmployeeRepository
    {
        private static List<Employee> _employees = new List<Employee>();

        static EmployeeRepository()
        {
            try
            {
                string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)));
                string fileName = "Employees.txt";
                string filePath = Path.Combine(path, fileName);

                if (!File.Exists(filePath))
                {
                    return;
                }

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Read();
                    _employees = csv.GetRecords<Employee>().ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool FirstAccess()
        {
            if(_employees.Any())
                return false;

            return true;
        }
        public void SaveAccess(string username)
        {
            var employee = _employees.FirstOrDefault(e => e.User.Username == username);

            employee.LastAccess = DateTime.Now;

            Save();
        }
        public bool AddEmployee(Employee employee)
        {
            if (_employees.Any(e => e.Document.Equals(employee.Document)))
            {
                Console.WriteLine("Funcionário já cadastrado.");
                Console.ReadKey();

                return false;
            }

            _employees.Add(new Employee(employee.Name, employee.Document, new User(UserRepository.ChangeUsername(_employees), UserRepository.ChangePassword(out var salt), salt)));

            Save();
            return true;
        }
        public void Save()
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)));
            string fileName = "Employees.txt";

            string filePath = Path.Combine(path, fileName);

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_employees);
            }
        }
        public bool IsLoginValid(string user, string password)
        {
            var employee = _employees.FirstOrDefault(e => e.User.Username == user);

            if (employee == null)
                return false;

            var hashedPassword = UserRepository.Hash(password, employee.User.Salt);

            if (_employees.Any(e => e.User.Username == user && hashedPassword== e.User.HashedPassword))
                return true;

            return false;
        }
        public bool GetInfos(int index, string info)
        {
            Employee employee;
            string situation = "Desativada";

            switch (index)
            {
                case 1: //By name
                    var employees = from e in _employees
                        where e.Name == info
                        select e;

                    if (!employees.Any())
                        return false;

                    foreach (var e in employees)
                    {
                        if (e.IsActive)
                            situation = "Ativada";
                        Console.Write($"\nNome: {e.Name}" +
                                      $"\nCPF: {e.Document}" +
                                      $"\nUsuário: {e.User.Username}" +
                                      $"\nSituação: {situation}" +
                                      $"\nÚltimo login:  {e.LastAccess}\n\n");
                    }
                    return true;
                case 2: //By document
                    long.TryParse(info, out var document);
                    employee = _employees.FirstOrDefault(e => e.Document == document);
                    break;
                default: //By username
                    employee = _employees.FirstOrDefault(e => e.User.Username == info);
                    break;
            }

            if (employee == null)
                return false;

            if (employee.IsActive)
                situation = "Ativada";

            Console.Write($"\nNome: {employee.Name}" +
                          $"\nCPF: {employee.Document}" +
                          $"\nUsuário: {employee.User.Username}" +
                          $"\nSituação: {situation}" +
                          $"\nÚltimo login:  {employee.LastAccess}\n\n");

            return true;
        }
        public bool ChangeData(long document, int index, string newData)
        {
            var employee = _employees.FirstOrDefault(e => e.Document == document);

            if (employee == null)
                return false;

            switch (index)
            {
                case 1:
                    employee.Name = newData;
                    break;
                case 2:
                    bool isBeingUsed = _employees.Any(c => c.Document == long.Parse(newData));
                    if (isBeingUsed)
                        return false;

                    employee.Document = long.Parse(newData);
                    break;
                case 3:
                    employee.User.Username = UserRepository.ChangeUsername(_employees);
                    break;
                case 4:
                    employee.User.HashedPassword = UserRepository.ChangePassword(out var salt);
                    employee.User.Salt = salt;
                    break;
            }
            Save();
            return true;
        }
        public bool DeactivateEmployee(long document)
        {
            var employee = _employees.FirstOrDefault(e => e.Document == document);

            if (employee == null)
                return false;

            if (employee.IsActive)
            {
                Console.WriteLine("A conta está ativa. Deseja desativar? (s/n)");
                var answer = Console.ReadLine();
                if (answer == "n")
                    return false;

                employee.IsActive = false;
            }
            else
            {
                Console.WriteLine("A conta está desativada. Deseja ativar? (s/n)");
                var answer = Console.ReadLine();
                if (answer == "n")
                    return false;

                employee.IsActive = true;
            }
            Save();
            return true;
        }
        public void CheckEmployeesAndLogin()
        {
            foreach (var employee in _employees)
            {
                if(!employee.IsActive)
                    continue;
                Console.WriteLine($"\nNome: {employee.Name}" +
                                  $"\nCPF: {employee.Document}" +
                                  $"\nUsuário: {employee.User.Username}" +
                                  $"\nÚltimo Login: {employee.LastAccess}");

                Console.WriteLine("\n---------- * ----------\n");
            }

            Console.WriteLine("Relatório finalizado.");
        }

        public void AddEmployeesList(List<Employee> employees)
        {
            foreach (var employee in employees)
                _employees.Add(employee);
            
            Save();
        }
    }
}
