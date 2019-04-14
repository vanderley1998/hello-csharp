using ByteBank_02.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank_02.Systems
{
    interface IAuthentication
    {
        bool Authenticator(string password);
    }
}

