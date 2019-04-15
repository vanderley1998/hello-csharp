using System;
using ByteBank.Exceptions;

namespace ByteBank
{
    class CheckingAccount
    {
        private double _balance;

        public CheckingAccount(int agency, int accountNumber)
        {
            if(agency <= 0)
            {
                throw new ArgumentException("The argument must be > than 0.", nameof(agency));
            }
            if (accountNumber <= 0)
            {
                throw new ArgumentException("The argument must be > than 0.", nameof(accountNumber));
            }
            this.Agency = agency;
            this.AccountNumber = accountNumber;
            TotalCheckingAccounts++;
        }

        public Customer Customer { get; set; }
        public static int TotalCheckingAccounts { get; private set; }

        public int Agency { get; }

        public int AccountNumber { get; }

        public double Balance
        {
            get
            {
                return this._balance;
            }

            set
            {
                if(value < 0)
                {
                    BalanceIsNotEnough innerException = new BalanceIsNotEnough(this.Balance, value);
                    throw new FinanceOperationException("Unsuccessful operation!", innerException);
                }
                this._balance = value;
            }
        }

        public void CashOut(double value)
        {
            if (this._balance < value)
            {
                BalanceIsNotEnough innerException = new BalanceIsNotEnough(this.Balance, value);
                throw new FinanceOperationException("Unsuccessful operation!", innerException);
            }
            else
            {
                this._balance -= value;
            }
        }

        public void CashDeposit(double value)
        {
            this._balance += value;
        }

        public void CashTransfer(double value, CheckingAccount otherAccount)
        {
            try
            {
                if (this._balance < value)
                {
                    BalanceIsNotEnough innerException = new BalanceIsNotEnough(this.Balance, value);
                    throw new FinanceOperationException("Unsuccessful operation!", innerException);
                }
                else
                {
                    this._balance -= value;
                    otherAccount.CashDeposit(value);
                }
            }
            catch(NullReferenceException)
            {
                Console.WriteLine("Null reference from " + nameof(otherAccount));
                // throw new NullReferenceException("Null reference from " + nameof(otherAccount)); // Perde informaçãoes do stacktracer
                throw; // Mantem o stacktracer
            }
        }

    }
}