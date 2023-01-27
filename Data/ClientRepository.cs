using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AdaCredit.UI.Domain;
using Bogus.DataSets;
using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using System.Text.RegularExpressions;
using CsvHelper.Configuration.Attributes;
using System.Xml.Linq;

namespace AdaCredit.UI.Data
{
    public class ClientRepository
    {
        private static List<Client> _clients = new List<Client>();

        static ClientRepository()
        {
            try
            {
                string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)));
                string fileName = "Clients.txt";
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
                    _clients = csv.GetRecords<Client>().ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Account> PassAccounts()
        {
            List<Account> accounts = _clients.Select(c => c.Account).ToList();
            return accounts;
        }

        public bool AddClient(Client client)
        {
            if (_clients.Any(c => c.Document.Equals(client.Document)))
            {
                System.Console.WriteLine("Cliente já cadastrado.");
                System.Console.ReadKey();

                return false;
            }

            var repository = new AccountRepository();
            _clients.Add(new Client(client.Name, client.Document, client.Phone, client.Address,
                repository.GetNewUnique()));

            Save();
            return true;
        }

        public void Save()
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)));
            string fileName = "Clients.txt";

            string filePath = Path.Combine(path, fileName);

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_clients);
            }
        }

        public bool GetInfos(int index, string? info, string? secondInfo = "")
        {
            Client? client = null;
            string situation = "Desativada";

            switch (index)
            {
                case 1:
                    var clients = from c in _clients
                        where c.Name == info
                        select c;

                    if (!clients.Any())
                        return false;

                    foreach (var c in clients)
                    {
                        if (c.Account.IsActive)
                            situation = "Ativada";
                        Console.Write($"Nome: {c.Name}" +
                                      $"\nCPF: {c.Document}" +
                                      $"\nTelefone: {c.Phone}" +
                                      $"\nEndereço: {c.Address}" +
                                      $"\nNúmero da conta: {c.Account.Number}" +
                                      $"\nAgência: {c.Account.Branch}" +
                                      $"\nSaldo: R${c.Account.Balance}" +
                                      $"\nSituação: {situation}\n\n");
                    }

                    return true;
                case 2:
                    long.TryParse(info, out var document);
                    client = _clients.FirstOrDefault(c => c.Document == document);
                    break;
                default:
                    client = _clients.FirstOrDefault(c => c.Account.Number == info && c.Account.Branch == secondInfo);
                    break;
            }

            if (client == null)
                return false;

            if (client.Account.IsActive)
                situation = "Ativada";

            Console.Write($"Nome: {client.Name}" +
                          $"\nCPF: {client.Document}" +
                          $"\nTelefone: {client.Phone}" +
                          $"\nEndereço: {client.Address}" +
                          $"\nNúmero da conta: {client.Account.Number}" +
                          $"\nAgência: {client.Account.Branch}" +
                          $"\nSaldo: R${client.Account.Balance}" +
                          $"\nSituação: {situation}\n\n");

            return true;
        }

        public bool ChangeData(long document, int index, string newData)
        {
            var client = _clients.FirstOrDefault(c => c.Document == document);

            if (client == null)
                return false;

            switch (index)
            {
                case 1:
                    client.Name = newData;
                    break;
                case 2:
                    bool isBeingUsed = _clients.Any(c => c.Document == long.Parse(newData));
                    if (isBeingUsed)
                        return false;
                    client.Document = long.Parse(newData);
                    break;
                case 3:
                    client.Phone = newData;
                    break;
                case 4:
                    client.Address = newData;
                    break;
                //case 5:
                //    var position = _clients.IndexOf(client);
                //    _clients[position] = new Client(client.Name, document, AccountRepository.GetNewUnique(_clients));
                //    Console.WriteLine($"A nova conta é {_clients[position].Account.Number}.");
            }

            Save();
            return true;
        }

        public bool DeactivateClient(long document)
        {
            var client = _clients.FirstOrDefault(c => c.Document == document);

            if (client == null)
                return false;

            if (client.Account.IsActive)
            {
                Console.WriteLine("A conta está ativa. Deseja desativar? (s/n)");
                var answer = Console.ReadLine();
                if (answer == "n")
                    return false;

                client.Account.IsActive = false;
            }
            else
            {
                Console.WriteLine("A conta está desativada. Deseja ativar? (s/n)");
                var answer = Console.ReadLine();
                if (answer == "n")
                    return false;

                client.Account.IsActive = true;
            }

            Save();
            return true;
        }

        public void ChargeValues(string accountNumber, string branch, decimal value, int direction)
        {
            accountNumber = accountNumber.Insert(5, "-");

            var client = _clients.FirstOrDefault(c => c.Account.Number == accountNumber && c.Account.Branch == branch);

            if (direction == 0)
                client.Account.Balance -= value;
            if (direction == 1)
                client.Account.Balance += value;

            Save();
        }

        public void CheckClientsAndBalances()
        {
            foreach (var client in _clients)
            {
                if (!client.Account.IsActive)
                    continue;

                Console.WriteLine($"\nNome: {client.Name}" +
                                  $"\nCPF: {client.Document}" +
                                  $"\nAgência: {client.Account.Branch}" +
                                  $"\nNúmero da conta: {client.Account.Number}" +
                                  $"\nSaldo: R${client.Account.Balance}");

                Console.WriteLine("\n---------- * ----------\n");
            }

            Console.WriteLine("Relatório finalizado.");
        }

        public void CheckInactiveClients()
        {
            foreach (var client in _clients)
            {
                if (client.Account.IsActive)
                    continue;

                Console.WriteLine($"\nNome: {client.Name}" +
                                  $"\nCPF: {client.Document}" +
                                  $"\nAgência: {client.Account.Branch}" +
                                  $"\nNúmero da conta: {client.Account.Number}");

                Console.WriteLine("\n---------- * ----------\n");
            }

            Console.WriteLine("Relatório finalizado.");
        }
    }
}
