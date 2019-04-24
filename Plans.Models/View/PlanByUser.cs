using Plans.Models.Plans;
using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.View
{
    public class PlanByUser
    {
        public PlanByUser(int USER_ID, string USER_NAME, int PLAN_ID, string PLAN_NAME, int ID_TYPE, int ID_STATUS, DateTime START_DATE, DateTime END_DATE, DateTime DATE_LATE_STATUS)
        {
            Plan = new Plan(PLAN_ID, PLAN_NAME, new PlanType(ID_TYPE), new User(USER_ID, USER_NAME), new PlanStatus(ID_STATUS), START_DATE, END_DATE);
            DateLateStatus = DATE_LATE_STATUS;
        }

        public Plan Plan { get; set; }
        public DateTime DateLateStatus { get; set; }

        public override string ToString()
        {
            return $"[User: {Plan.User.Id}, Plan: {Plan.Id}, DateLateStatus: {DateLateStatus}]";
        }
    }
}
