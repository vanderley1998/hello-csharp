using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer c1 = new Customer("123123123");
            c1.Name = "Vanderley Sousa";
            c1.Occupation = "Dev";

            Customer c2 = new Customer("242344345");
            c2.Name = "Renan Fermino";
            c2.Occupation = "Suporte";

            CheckingAccount account = new CheckingAccount(1231, 654878);
            account.Customer = c1;
            account.balance = 200;

            CheckingAccount account2 = new CheckingAccount(1231, 416512);
            account2.Customer = c2;
            account2.balance = 100;

            Console.WriteLine(account2.balance);

            Console.ReadLine();
        }
    }
}
