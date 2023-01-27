using AdaCredit.UI.Data;
using AdaCredit.UI.Domain;
using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace AdaCredit.UI.Testing
{
    public class CreateEmployees
    {
        public static void Execute()
        {
            
            bool flag = false;
            var employees = new List<Employee>();
            var count = 0;

            while(count < 20)
            {
                string salt = "";
                var gender = new Faker().PickRandom<Name.Gender>();
                var firstName= new Faker("pt_BR").Name.FirstName(gender);
                var lastName = new Faker("pt_BR").Name.LastName(gender);
                var name = firstName + " " + lastName;
                var document = long.Parse(new Faker().Random.ReplaceNumbers("###########"));
                var username = new Faker("pt_BR").Internet.UserName(firstName, lastName);
                var hashedPass = UserRepository.GenerateHash(new Faker("pt_BR").Internet.Password(), out salt);
                
                if(employees.Any(e => e.User.Username == username || e.Document == document))
                    continue;

                employees.Add(new Employee(name, document, new User(username, hashedPass, salt)));
                count++;
            } 

            var repository = new EmployeeRepository();
            repository.AddEmployeesList(employees);
        }
    }
}
