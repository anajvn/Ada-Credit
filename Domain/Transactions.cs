using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.Domain
{
    public class Transactions
    {
        public int SourceBank { get; set; }
        public string SourceBranch { get; set; }
        public string SourceAccountNumber { get; set; }

        public int DestinationBank { get; set; }
        public string DestinationBranch { get; set; }
        public string DestinationAccountNumber { get; set; }

        public string Type { get; set; }
        public int Direction { get; set; }
        public decimal Value { get; set; }

        public Transactions(int sourceBank, string sourceBranch, string sourceAccountNumber, int destinationBank, string destinationBranch, string destinationAccountNumber, string type, int direction, decimal value)
        {
             SourceBank = sourceBank;
            SourceBranch = sourceBranch;
            SourceAccountNumber = sourceAccountNumber;
            DestinationBank = destinationBank;
            DestinationBranch = destinationBranch;
            DestinationAccountNumber = destinationAccountNumber;
            Type = type;
            Direction = direction;
            Value = value;
        }
        public override string ToString()
        {
            return $"{SourceBank},{SourceBranch},{SourceAccountNumber},{DestinationBank},{DestinationBranch},{DestinationAccountNumber},{Type},{Direction},{Value}";
        }
    }
}
