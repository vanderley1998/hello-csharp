using ByteBank_02.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank_02
{
    class Program
    {
        static void Main(string[] args)
        {

            Director employee = new Director(5000, "123.234.346-89");
            employee.Name = "Vanderley Sousa";
            employee.Password = "123";
            employee.IncreaseSalary();
            bool flag = employee.Authenticator("123");
            Console.WriteLine(flag);
            Console.ReadLine();

        }
    }
}
