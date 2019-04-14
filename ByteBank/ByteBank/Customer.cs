using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank
{
    class Customer {

        private string _cpf;

        public Customer(string cpf)
        {
            this.Cpf = cpf;
        }

        public string Name { get; set; }
        public string Occupation { get; set;  }
        
        public string Cpf
        {
            get
            {
                return this._cpf;
            }
            set
            {
                // TODO validation
                this._cpf = value;
            }
        }

    }
}
