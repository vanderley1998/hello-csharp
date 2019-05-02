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
            int affectedLines = PlanModuleDB.ConnectionDB.Execute($"DELETE FROM PLAN_STATUS WHERE ID = @Id", new { Id = id });
            return affectedLines > 0;
        }

        public PlanStatus Get(int id)
        {
            try
            {
                var planStatusFound = PlanModuleDB.ConnectionDB
                    .Query<PlanStatus>(@"
                        SELECT * FROM PLAN_STATUS
                        WHERE ID = @id
                    ", param: new { id });
                return planStatusFound.First();
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }

        public IEnumerable<PlanStatus> GetAll()
        {
            IEnumerable<PlanStatus> list = PlanModuleDB.ConnectionDB.Query<PlanStatus>("SELECT * FROM PLAN_STATUS");
            return list;
        }

        public IEnumerable<PlanStatus> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public PlanStatus Save(PlanStatus obj)
        {
            string query;
            if(obj.Id == 0)
            {
                query = "INSERT INTO PLAN_STATUS (NAME) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int);";
                var planStatusInserted = PlanModuleDB.ConnectionDB.Query<int>(query, param: new { obj.Name });
                obj.Id = planStatusInserted.Single();
                return obj;
            }
            else
            {
                query = @"UPDATE PLAN_STATUS SET NAME = @Name WHERE ID = @Id";
                int affectedLines = PlanModuleDB.ConnectionDB.Execute(query, param: new { obj.Name, obj.Id });
                return affectedLines > 0 ? obj : throw new ArgumentException($"There's no PlanStatus with id = {obj.Id} in database.");
            }
        }
    }
}
