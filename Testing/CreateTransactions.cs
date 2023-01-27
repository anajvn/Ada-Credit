using System;
using AdaCredit.UI.Data;
using Bogus;
using AdaCredit.UI.Domain;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace AdaCredit.UI.Testing
{
    public class CreateTransactions
    {
        public static void Execute()
        {
            string mainPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Transactions\\Pending");

            if (!Directory.Exists(mainPath))
                Directory.CreateDirectory(mainPath);

            int count = 0;
            while (count < 5)
            {
                // Create file path and name
                var bankName = new Faker().Company.CompanyName(0).Replace(" ", "-").Replace(".", "");
                var date = (new Faker().Date.RecentDateOnly(100)).ToString("yyyyMMdd");
                var fileName = $"{bankName}-{date}.csv";

                // Create file content
                List<Transactions> transactions = new List<Transactions>();
                for (int i = 0; i < 5; i++)
                {
                    var rdm = new Random().Next(0, 3);
                    string[] type = new string[]{ "TED", "DOC", "TEF" };
                    var typeIndex = new Random().Next(0,3);
                    var direction = new Random().Next(0,2);
                    decimal value = decimal.Parse(new Faker("en_US").Commerce.Price(1M, 100000M));
                    int sourceBank, destinationBank;
                    string sourceNumber, sourceBranch, destinationBranch, destinationNumber;

                    if (rdm == 0) // Origin account will be from Ada
                    {
                        sourceBank = GenerateAda(out sourceNumber, out sourceBranch);
                        destinationBank = GenerateRandom(out destinationNumber, out destinationBranch);
                    }
                    else if (rdm == 1) // Destination account will be from Ada
                    {
                        sourceBank = GenerateRandom(out sourceNumber, out sourceBranch);
                        destinationBank = GenerateAda(out destinationNumber, out destinationBranch);
                    }
                    else // Both account will be from Ada
                    {
                        sourceBank = GenerateAda(out sourceNumber, out sourceBranch);
                        destinationBank = GenerateAda(out destinationNumber, out destinationBranch);
                    }
                    transactions.Add(new Transactions(sourceBank, sourceBranch, sourceNumber, destinationBank, destinationBranch, destinationNumber, type[typeIndex], direction, value));
                }
                var filePath = Path.Combine(mainPath, fileName);
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(transactions);
                }
                count++;
            }
        }
        static int GenerateAda(out string number, out string branch)
        {
            var repository = new ClientRepository();
            var accounts = repository.PassAccounts();
            var index = new Random().Next(0, accounts.Count);

            var account = accounts[index];
            number = account.Number.Remove(5, 1);
            branch = account.Branch;
            return 777;
        }

        static int GenerateRandom(out string number, out string branch)
        {
            number = new Faker().Random.ReplaceNumbers("######");
            branch = new Faker().Random.ReplaceNumbers("####");
            int bank = Int32.Parse(new Faker().Random.ReplaceNumbers("###"));

            return bank;
        }
    }
}
