using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using Plans.Models.Plans;

namespace Plans.Database
{
    public class DataInnerPlan : ICrud<InnerPlan>
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public InnerPlan Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InnerPlan> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InnerPlan> GetById(int idParentPlan)
        {
            try
            {
                var innerPlanFound = PlanModuleDB.ConnectionDB.Query<InnerPlan>(@"
                    SELECT
                        ID,
                        ID_PARENT_PLAN AS ParentPlan,
                        ID_CHILD_PLAN AS ChildPlan
                    FROM INNER_PLANS
                    WHERE ID_PARENT_PLAN = @idParentPlan
                ", param: new { idParentPlan });
                return innerPlanFound;
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }

        public InnerPlan Save(InnerPlan obj)
        {
            throw new NotImplementedException();
        }
    }
}
