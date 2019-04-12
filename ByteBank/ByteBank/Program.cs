using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ByteBank;

namespace _01_ByteBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer c1 = new Customer();
            c1.name = "Vanderley Sousa";
            c1.cpf = "123123123";
            c1.occupation = "Dev";

            Customer c2 = new Customer();
            c2.name = "Renan Fermino";
            c2.cpf = "242344345";
            c2.occupation = "Suporte";

            CheckingAccount account = new CheckingAccount();
            account.customer = c1;
            account.balance = 200;

            CheckingAccount account2 = new CheckingAccount();
            account2.customer = c2;
            account2.balance = 100;

            bool flag = account.CashTransfer(100, account2);
            Console.WriteLine(account2.balance);
            Console.WriteLine(flag);

            Console.ReadLine();
        }
    }
}
