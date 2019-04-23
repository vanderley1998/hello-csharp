using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Plans.Models.Users;

namespace Plans.Database
{
    public class DataPlan : ICrud<Plan>
    {
        public bool Delete(int id)
        {
            int affectedLines = PlanModuleDB.OpenConnection().Execute($"EXEC DELETE_PLAN_REGISTERS_FROM_ALL_TABLES @ID = @Id", new { Id = id });
            return affectedLines > 0;
        }

        public IEnumerable<Plan> GetAll()
        {
            IEnumerable<Plan> list = PlanModuleDB.OpenConnection()
                .Query<Plan, User, Plan>("SELECT * FROM PLANS P INNER JOIN USERS U ON U.ID = P.ID",
                    map: (plan, user) =>
                    {
                        plan.User = user;
                        return plan;
                    }
                );
            return list;
        }

        public Plan Save(Plan obj)
        {
            throw new NotImplementedException();
        }
    }
}
