using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Database
{
    public class Connection
    {

        public static IDbConnection OpenConnection()
        {
            return new SqlConnection("Data Source=localhost; Initial Catalog=PLANS_MODULE; Integrated Security=false; User Id=sa; Password=flordodia;");
        }

    }
}
