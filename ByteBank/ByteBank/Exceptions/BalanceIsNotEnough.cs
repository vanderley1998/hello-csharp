using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Exceptions
{
    class BalanceIsNotEnough : FinanceOperationException
    {
        public double CurrentBalance { get; }
        public double AttemptValue { get; }

        public BalanceIsNotEnough()
        {

        }

        public BalanceIsNotEnough(string message) : base(message)
        {
        }

        public BalanceIsNotEnough(double currentBalance, double attemptValue)
            : this("The current balance ("+currentBalance+") is < than the attempt value ("+attemptValue+").")
        {
            this.CurrentBalance = currentBalance;
            this.AttemptValue = attemptValue;
        }

        public BalanceIsNotEnough(string message, Exception innerException)
            :base(message, innerException)
        {

        }

    }
}
