using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaCredit.UI.Data;
using AdaCredit.UI.Domain;
using Bogus;
using Bogus.DataSets;

namespace AdaCredit.UI.Testing
{
    public class CreateClients
    {
        public static void Execute()
        {
            var path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)));
            var filePath = Path.Combine(path, "Clients.txt");
            if (File.Exists(filePath))
                return;

            bool flag = false;
            var clients = new List<Client>();
            var count = 0;

            while (count < 100)
            {
                var gender = new Faker().PickRandom<Name.Gender>();
                var firstName = new Faker("pt_BR").Name.FirstName(gender);
                var lastName = new Faker("pt_BR").Name.LastName(gender);
                var name = firstName + " " + lastName;
                var document = long.Parse(new Faker().Random.ReplaceNumbers("###########"));
                var phone= new Faker("pt_BR").Person.Phone;
                var address= new Faker("pt_BR").Address.FullAddress();

                if (clients.Any(c => c.Document == document))
                    continue;

                clients.Add(new Client(name, document, phone, address));
                count++;
            }

            var repository = new ClientRepository();
            foreach (var client in clients)
            {
                repository.AddClient(client);
            }
        }

    }
}
