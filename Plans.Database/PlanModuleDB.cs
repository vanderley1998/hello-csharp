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
        private static string StringConnection { get; set; }
        public DataPlan DataPlan { get; private set; }
        public DataUser DataUser { get; private set; }
        public DataPlanStatus DataPlanStatus { get; private set; }
        public DataPlanType DataPlanType { get; private set; }
        public DataPlansHistory DataPlansHistory { get; private set; }
        public DataUsersHistory DataUsersHistory { get; private set; }

        public PlanModuleDB(string conn)
        {
            if (conn.Equals(""))
            {
                throw new ArgumentException("A string connection is required and cannot be empty.");
            }
            StringConnection = conn;
            DataPlan = new DataPlan();
            DataUser = new DataUser();
            DataPlanStatus = new DataPlanStatus();
            DataPlanType = new DataPlanType();
            DataPlansHistory = new DataPlansHistory();
            DataUsersHistory = new DataUsersHistory();
        }

        internal static SqlConnection OpenConnection()
        {
            return new SqlConnection(StringConnection);
        }
    }
}
