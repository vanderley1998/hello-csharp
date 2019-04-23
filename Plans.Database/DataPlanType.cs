using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

namespace Plans.Database
{
    public class DataPlanType : ICrud<PlanType>
    {
        public bool Delete(int id)
        {
            int affectedLines = PlanModuleDB.OpenConnection().Execute($"DELETE FROM PLAN_TYPES WHERE ID = @Id", new { Id = id });
            return affectedLines > 0;
        }

        public IEnumerable<PlanType> GetAll()
        {
            IEnumerable<PlanType> list = PlanModuleDB.OpenConnection().Query<PlanType>("SELECT * FROM PLAN_TYPES");
            return list;
        }

        public PlanType Save(PlanType obj)
        {
            throw new NotImplementedException();
        }
    }
}
