using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ByteBank.Models;

namespace Program
{
    class Receptionist : Employee
    {

        public Receptionist(double salary, string cpf)
            : base(salary, cpf)
        {

        }

        public override double GetBonus()
        {
            throw new NotImplementedException();
        }

        public override void IncreaseSalary()
        {
            throw new NotImplementedException();
        }
    }
}
