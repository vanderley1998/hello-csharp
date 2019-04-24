using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string query;
            if(obj.Id == 0)
            {
                query = "INSERT INTO PLAN_STATUS (NAME) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int);";
                var planStatusInserted = PlanModuleDB.OpenConnection().Query<int>(query, param: new { obj.Name });
                obj.Id = planStatusInserted.Single();
                return obj;
            }
            else
            {
                query = @"UPDATE PLAN_STATUS SET NAME = @Name WHERE ID = @Id";
                int affectedLines = PlanModuleDB.OpenConnection().Execute(query, param: new { obj.Name, obj.Id });
                return affectedLines > 0 ? obj : throw new ArgumentException($"There's no PlanStatus with id = {obj.Id} in database.");
            }
        }
    }
}
