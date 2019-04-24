using Plans.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

namespace Plans.Database.View
{
    public class ViewTotalPlansByUser : ICrud<TotalPlansByUser>
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TotalPlansByUser> GetAll()
        {
            IEnumerable<TotalPlansByUser> list = PlanModuleDB.OpenConnection()
                .Query<TotalPlansByUser>("SELECT * FROM VIEW_TOTAL_PLANS_BY_USER");
            return list;
        }

        public TotalPlansByUser Save(TotalPlansByUser obj)
        {
            throw new NotImplementedException();
        }
    }
}
