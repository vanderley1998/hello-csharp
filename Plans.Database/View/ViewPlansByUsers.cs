using Plans.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

namespace Plans.Database.View
{
    public class ViewPlansByUsers : ICrud<PlanByUser>
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlanByUser> GetAll()
        {
            IEnumerable<PlanByUser> list = PlanModuleDB.OpenConnection()
                .Query<PlanByUser>(@"
                    SELECT USER_ID, USER_NAME, PLAN_ID, PLAN_NAME, ID_TYPE, ID_STATUS, START_DATE, END_DATE, DATE_LATE_STATUS
                    FROM VIEW_PLANS_BY_USERS
                ");
            return list;
        }

        public PlanByUser Save(PlanByUser obj)
        {
            throw new NotImplementedException();
        }
    }
}
