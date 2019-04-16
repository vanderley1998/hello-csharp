using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Models
{
    interface IAuthentication
    {
        bool Authenticator(string password);
    }
}

