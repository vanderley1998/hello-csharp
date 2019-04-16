namespace ByteBank.Models
{
    class Customer {

        public Customer(string cpf)
        {
            this.Cpf = cpf;
        }

        public string Name { get; set; }
        public string Occupation { get; set;  }
        public string Cpf { get; }

    }
}
