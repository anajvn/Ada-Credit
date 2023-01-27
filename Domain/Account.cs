using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace AdaCredit.UI.Domain
{
    public sealed class Account
    {
        public string Number { get; private set; }
        public string Branch { get; private set; }
        public decimal Balance { get; set; } = 0M;
        public bool IsActive { get; set; } = true;

        public Account(string number)
        {
            Number = number;
            Branch = "0001";
        }

        public Account(string number, string branch, decimal balance, bool isActive)
        {
            Number = number;
            Branch = branch;
            Balance = balance;
            IsActive = isActive;
        }
    }
}
