using ByteBank_02.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank_02.Employees
{
    class Director : Employee, IAuthentication
    {

        public Director(double salary, string cpf): base(salary, cpf)
        {

        }

        public bool Authenticator(string password)
        {
            return this.Password.Equals(password);
        }

        public override double GetBonus()
        {
            return this.Salary;
        }

        public override void IncreaseSalary()
        {
            this.Salary *= 1.1;
        }

    }
}
