using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Database
{
    public class PlanModuleDB
    {

        public UserData UserData { get; private set; }
        public PlanData PlanData { get; private set; }

        public PlanModuleDB(string conn)
        {
            if (conn.Equals(""))
            {
                throw new ArgumentException("A string connection is required and cannot be empty.");
            }
            UserData = new UserData(OpenConnection(conn));
            PlanData = new PlanData(OpenConnection(conn));
        }

        private SqlConnection OpenConnection(string conn)
        {
            return new SqlConnection(conn);
        }
    }
}
