namespace ByteBank
{
    class CheckingAccount
    {
        public Customer customer;
        public int agency;
        public int accountNumber;
        public double balance;

        public bool CashOut(double value)
        {
            if (this.balance < value)
            {
                return false;
            }
            else
            {
                this.balance -= value;
                return true;
            }
        }

        public void CashDeposit(double value)
        {
            this.balance += value;
        }

        public bool CashTransfer(double value, CheckingAccount otherAccount)
        {
            if (this.balance < value)
            {
                return false;
            }
            else
            {
                this.balance -= value;
                otherAccount.CashDeposit(value);
                return true;
            }
        }
    }
}