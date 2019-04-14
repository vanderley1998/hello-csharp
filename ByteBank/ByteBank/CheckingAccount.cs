namespace ByteBank
{
    class CheckingAccount
    {
        private int _agency;
        private int _accountNumber;
        private double _balance;

        public CheckingAccount(int agency, int accountNumber)
        {
            this.Agency = agency;
            this.AccountNumber = accountNumber;
            TotalCheckingAccounts++;
        }

        public Customer Customer { get; set; }
        public static int TotalCheckingAccounts { get; private set; }

        public int Agency
        {
            get
            {
                return this._agency;
            }

            set
            {
                // TODO Agency validation
                this._agency = value;
            }
        }

        public int AccountNumber
        {
            get
            {
                return this._accountNumber;
            }
            set
            {
                // TODO account number validation
                this._accountNumber = value;
            }
        }

        public double balance
        {
            get
            {
                return this._balance;
            }

            set
            {
                if(value < 0)
                {
                    return;
                }
                this._balance = value;
            }
        }

        public bool CashOut(double value)
        {
            if (this._balance < value)
            {
                return false;
            }
            else
            {
                this._balance -= value;
                return true;
            }
        }

        public void CashDeposit(double value)
        {
            this._balance += value;
        }

        public bool CashTransfer(double value, CheckingAccount otherAccount)
        {
            if (this._balance < value)
            {
                return false;
            }
            else
            {
                this._balance -= value;
                otherAccount.CashDeposit(value);
                return true;
            }
        }

    }
}