using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaCredit.UI.Domain;
using Bogus;

namespace AdaCredit.UI.Data
{
    public class AccountRepository
    {
        private static List<Account> _accounts = new List<Account>();

        static AccountRepository()
        {
            var repository = new ClientRepository();
            _accounts = repository.PassAccounts();
        }
        public Account GetNewUnique()
        {
            var exists = false;
            var accountNumber = "";

            do
            {
                accountNumber = new Faker().Random.ReplaceNumbers("#####-#");
                exists = _accounts.Any(a => a.Number.Equals(accountNumber));

            } while (exists);

            return new Account(accountNumber);
        }

        public Account ReturnRandomAccount()
        {
            var index = new Random().Next(0, _accounts.Count);
            return _accounts[index];
        }
        public bool IsAccountValid( string accountNumber, string branch)
        {
            accountNumber = accountNumber.Insert(5, "-");
            bool flag = _accounts.Any(a => a.Number == accountNumber && a.Branch == branch);

            return flag;
        }
        public bool IsAccountActive(string accountNumber, string branch)
        {
            accountNumber = accountNumber.Insert(5, "-");
            var account = _accounts.FirstOrDefault(a => a.Number == accountNumber && a.Branch == branch);
            
            return account.IsActive;
        }
        public bool IsBalanceEnough(string accountNumber, string branch, decimal value)
        {
            accountNumber = accountNumber.Insert(5, "-");
            bool result = false;
            var account = _accounts.FirstOrDefault(a => a.Number == accountNumber && a.Branch == branch);
            if (account.Balance > value)
                result = true;

            return result;
        }
    }
}
