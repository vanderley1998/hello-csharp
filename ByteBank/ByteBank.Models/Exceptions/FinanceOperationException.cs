using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Models
{
    class FinanceOperationException : Exception
    {

        public FinanceOperationException()
        {

        }

        public FinanceOperationException(string message)
            : base(message)
        {

        }

        public FinanceOperationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

    }
}
