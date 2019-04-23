using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Plans.Database
{
    public class DataPlanStatus : ICrud<PlanStatus>
    {
        public bool Delete(int id)
        {
            int affectedLines = PlanModuleDB.OpenConnection().Execute($"DELETE FROM PLAN_STATUS WHERE ID = @Id", new { Id = id });
            return affectedLines > 0;
        }

        public IEnumerable<PlanStatus> GetAll()
        {
            IEnumerable<PlanStatus> list = PlanModuleDB.OpenConnection().Query<PlanStatus>("SELECT * FROM PLAN_STATUS");
            return list;
        }

        public PlanStatus Save(PlanStatus obj)
        {
            if(obj.Id == 0)
            {
                var planStatusInserted = PlanModuleDB.OpenConnection()
                    .Query<int>(@"
                        INSERT INTO PLAN_STATUS (NAME) VALUES (@Name);
                        SELECT CAST(SCOPE_IDENTITY() as int)", param: new { obj.Name }
                    );
                obj.Id = planStatusInserted.Single();
                Console.WriteLine(obj);
                return obj;
            }
            return null;
        }
    }
}
