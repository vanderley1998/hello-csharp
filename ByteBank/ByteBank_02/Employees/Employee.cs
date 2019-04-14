using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank_02.Employees
{
    abstract class Employee
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public double Salary { get; protected set; }
        public string Password { get; set; }

        public Employee(double salary, string cpf)
        {
            this.Salary = salary;
            this.CPF = cpf;
        }

        public abstract double GetBonus();
        public abstract void IncreaseSalary();

    }
}
