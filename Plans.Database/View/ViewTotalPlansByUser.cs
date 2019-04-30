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

        public TotalPlansByUser Get(int id)
        {
            try
            {
                IEnumerable<TotalPlansByUser> list = PlanModuleDB.ConnectionDB
                .Query<TotalPlansByUser>(@"
                    SELECT * FROM VIEW_TOTAL_PLANS_BY_USER
                    WHERE USER_ID = @id
                ", param: new { id });
                return list.First();
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }

        public IEnumerable<TotalPlansByUser> GetAll()
        {
            IEnumerable<TotalPlansByUser> list = PlanModuleDB.ConnectionDB
                .Query<TotalPlansByUser>("SELECT * FROM VIEW_TOTAL_PLANS_BY_USER");
            return list;
        }

        public IEnumerable<TotalPlansByUser> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TotalPlansByUser Save(TotalPlansByUser obj)
        {
            throw new NotImplementedException();
        }
    }
}
