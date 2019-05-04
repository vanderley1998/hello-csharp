using Plans.Database.View;
using Plans.Models.View;
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
        public DataPlan DataPlan { get; private set; }
        public DataUser DataUser { get; private set; }
        public DataPlanStatus DataPlanStatus { get; private set; }
        public DataPlanType DataPlanType { get; private set; }
        public DataPlansHistory DataPlansHistory { get; private set; }
        public DataUserHistory DataUsersHistory { get; private set; }
        public DataPlanInterestedUsers DataPlanInterestedUsers { get; private set; }
        public DataInnerPlan DataInnerPlan { get; private set; }
        public ViewInterestedUsersByPlan ViewInterestedUsersByPlan { get; private set; }
        public ViewTotalPlansByUser ViewTotalPlansByUser { get; set; }
        public ViewPlansByUsers ViewPlansByUsers { get; set; }
        public static SqlConnection ConnectionDB { get; set; }
        public PlanModuleDB(string conn)
        {
            if (conn.Equals(""))
            {
                throw new ArgumentException("A string connection is required and cannot be empty.");
            }
            DataPlan = new DataPlan();
            DataUser = new DataUser();
            DataPlanStatus = new DataPlanStatus();
            DataPlanType = new DataPlanType();
            DataPlansHistory = new DataPlansHistory();
            DataUsersHistory = new DataUserHistory();
            DataPlanInterestedUsers = new DataPlanInterestedUsers();
            DataInnerPlan = new DataInnerPlan();
            ViewInterestedUsersByPlan = new ViewInterestedUsersByPlan();
            ViewTotalPlansByUser = new ViewTotalPlansByUser();
            ViewPlansByUsers = new ViewPlansByUsers();
            ConnectionDB = new SqlConnection(conn);
        }
    }
}
