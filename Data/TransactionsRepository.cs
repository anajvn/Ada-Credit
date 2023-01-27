using AdaCredit.UI.Domain;
using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Bogus.DataSets;
using Microsoft.Win32;
using System.Runtime.ConstrainedExecution;

namespace AdaCredit.UI.Data
{
    public class TransactionsRepository
    {
        static TransactionsRepository() { }
        public bool Failure(Transactions transaction, out string reason)
        {
            // If returns true, there is failure
            // If returns false, everything is correct

            if (transaction.Type == "TEF" && transaction.SourceBank != 777 && transaction.DestinationBank != 777)
            {
                reason = "TEF deve ocorrer entre clientes do mesmo banco.";
                return true;
            } 
            else if (transaction.SourceBank != 777 && transaction.DestinationBank != 777)
            {
                reason = "Nenhuma das contas envolvidas é deste banco.";
                return true;
            }
            else if(!AccountNumberCheck(1, transaction))
            {
                reason = "Conta de origem tem dados inválidos ou inexistentes";
                return true;
            } 
            else if (!AccountNumberCheck(2, transaction))
            {
                reason = "Conta de destino tem dados inválidos ou inexistentes";
                return true;
            }
            else if (!AccountSituationCheck(1, transaction))
            {
                reason = "Conta de origem está desativada.";
                return true;
            }
            else if (!AccountSituationCheck(2, transaction))
            {
                reason = "Conta de destino está desativada.";
                return true;
            } 
            else if (!BalanceCheck(1, transaction))
            {
                reason = "Conta de origem não tem saldo suficiente.";
                return true;
            }
            else if (!BalanceCheck(2, transaction))
            {
                reason = "Conta de destino não tem saldo suficiente.";
                return true;
            }

            reason = "";
            return false;
        }
        public bool AccountNumberCheck(int index, Transactions transaction)
        {
            // Index = 1 if checking is to source bank
            // Index = 2 if checking is to destination bank

            var repository = new AccountRepository();
            bool result = true;
            if (index == 1 && transaction.SourceBank == 777)
                result = repository.IsAccountValid(transaction.SourceAccountNumber, transaction.SourceBranch);
            else if (index == 2 && transaction.DestinationBank == 777)
                result = repository.IsAccountValid(transaction.DestinationAccountNumber, transaction.DestinationBranch);

            return result;
        }
        public bool AccountSituationCheck(int index, Transactions transaction)
        {
            // Index = 1 if checking is to source bank
            // Index = 2 if checking is to destination bank

            var repository = new AccountRepository();
            bool result = true;
            if (index == 1 && transaction.SourceBank == 777)
                result = repository.IsAccountActive( transaction.SourceAccountNumber, transaction.SourceBranch);
            else if (index == 2 && transaction.DestinationBank == 777)
                result = repository.IsAccountActive(transaction.DestinationAccountNumber, transaction.DestinationBranch);

            return result;
        }
        public bool BalanceCheck(int index, Transactions transaction)
        {
            // Index = 1 if checking is to source bank
            // Index = 2 if checking is to destination bank

            var repository = new AccountRepository();
            bool result = true;
            if (index == 1 && transaction.SourceBank == 777 && transaction.Direction == 0)
                result = repository.IsBalanceEnough(transaction.SourceAccountNumber, transaction.SourceBranch,
                    transaction.Value);
            else if (index == 2 && transaction.DestinationBank == 777 && transaction.Direction == 1)
                result = repository.IsBalanceEnough(transaction.DestinationAccountNumber, transaction.DestinationBranch,
                    transaction.Value);

            return result;
        }
        public void FailureFileSave(List<Transactions> transactions, string file, Dictionary<string, string> reasons)
        {
            if(!transactions.Any())
                return;

            string originName = Path.GetFileName(file);
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Transactions\\Failed");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string newFileName = originName.Substring(0, originName.Length - 4) + "-failed.csv";

            string filePath = Path.Combine(path, newFileName);
            
            using (FileStream fs = File.Create(filePath)) { }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header.
                HasHeaderRecord = false,
            };
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(transactions);
            }

            string failed = "Failed.txt";
            string failedPath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory))), failed);

            if (!File.Exists(failedPath))
                using (FileStream fs = File.Create(failedPath)){ }

            foreach (KeyValuePair<string, string> kvp in reasons)
            {
                File.AppendAllText(failedPath,
                    GetDate(file) + ", " + kvp.Key +", "+kvp.Value + Environment.NewLine);
            }
        }
        public void CompletedFileSave(List<Transactions> transactions, string file)
        {
            if (!transactions.Any())
                return;

            string originName = Path.GetFileName(file);
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Transactions\\Completed");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string newFileName = originName.Substring(0, originName.Length - 4) + "-completed.csv";

            string filePath = Path.Combine(path, newFileName);


            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header.
                HasHeaderRecord = false,
            };
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(transactions);
            }
        }
        public DateOnly GetDate(string file)
        {
            string fileName = Path.GetFileName(file);
            int initialIndex = fileName.Length - 12;
            string date = fileName.Substring(initialIndex, 8);

            DateOnly dateFormated = DateOnly.ParseExact(date, "yyyyMMdd");

            return dateFormated;
        }
        public void ApplyValues(Transactions transaction, DateOnly date)
        {
            var valueWithTaxes = ApplyTaxes(transaction.Type, transaction.Direction, date, transaction.Value);
            var repository = new ClientRepository();
            if (transaction.SourceBank == 777)
                repository.ChargeValues(transaction.SourceAccountNumber, transaction.SourceBranch, valueWithTaxes, transaction.Direction);

            if (transaction.DestinationBank == 777)
            {
                int directionFixed = 0;
                if(transaction.Direction == 0)
                    directionFixed = 1;
                repository.ChargeValues(transaction.DestinationAccountNumber, transaction.DestinationBranch, valueWithTaxes, directionFixed);
            }
        }
        public decimal ApplyTaxes(string type, int direction, DateOnly date, decimal value) // return value after taxes
        {
            DateOnly dateToCompare = new DateOnly(2022, 11, 30);

            if (direction == 1 || date.CompareTo(dateToCompare) < 0|| type=="TEF")
                return value;
            if (type == "TED")
                return value - 5.0M;
            else
            {
                decimal variableTax = 0.01M * value;
                if (variableTax > 5.00M)
                    variableTax = 5.0M;
                return value - 1.0M - variableTax;
            }
        }
        public bool Process()
        {
            string mainPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Transactions\\Pending");

            if (!Directory.Exists(mainPath))
            {
                Directory.CreateDirectory(mainPath);
                return false;
            }

            var files = Directory.GetFiles(mainPath);

            foreach (var file in files)
            {
                List<Transactions> records;
                List<Transactions> failures = new List<Transactions>();
                List<Transactions> completed = new List<Transactions>();
                Dictionary<string, string> reasons = new Dictionary<string, string>();

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, config))
                {
                    records = csv.GetRecords<Transactions>().ToList();
                }
                foreach (var record in records)
                {
                    string reason;
                    var flag = Failure(record, out reason);
                    if (flag)
                    {

                        reasons.Add(record.ToString(), reason);
                        failures.Add(record);
                        continue; 
                    }
                    completed.Add(record);
                    ApplyValues(record, GetDate(file));
                }
                CompletedFileSave(completed, file);
                FailureFileSave(failures, file, reasons);
                File.Delete(file);
            }
            Directory.Delete(mainPath);
            return true;
        }
        public static void FailedTransactions()
        {
            string failed = "Failed.txt";
            string failedPath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory))), failed);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ", "
            };
            using var reader = new StreamReader(failedPath);
            using var csv = new CsvParser(reader, config);
            {
                while (csv.Read())
                {
                    Console.WriteLine($"Data: {csv.Record[0]}" +
                                      $"\nTransação: {csv.Record[1]}" +
                                      $"\nMotivo: {csv.Record[2]}");
                    Console.WriteLine("\n---------- * ----------\n");
                }
            }
            Console.WriteLine("Relatório finalizado.");
        }
    }
}
