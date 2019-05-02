using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;

using Dapper;

namespace Plans.Database
{
    public class DataPlanType : ICrud<PlanType>
    {
        public bool Delete(int id)
        {
            int affectedLines = PlanModuleDB.ConnectionDB.Execute($"DELETE FROM PLAN_TYPES WHERE ID = @Id", new { Id = id });
            return affectedLines > 0;
        }

        public PlanType Get(int id)
        {
            try
            {
                var planTypeFound = PlanModuleDB.ConnectionDB
                    .Query<PlanType>(@"
                        SELECT * FROM PLAN_TYPES
                        WHERE ID = @id
                    ", param: new { id });
                return planTypeFound.First();
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }

        public IEnumerable<PlanType> GetAll()
        {
            IEnumerable<PlanType> list = PlanModuleDB.ConnectionDB.Query<PlanType>("SELECT * FROM PLAN_TYPES");
            return list;
        }

        public IEnumerable<PlanType> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public PlanType Save(PlanType obj)
        {
            string query;
            if (obj.Id == 0)
            {
                query = "INSERT INTO PLAN_TYPES (NAME) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int);";
                var planTypeInserted = PlanModuleDB.ConnectionDB.Query<int>(query, param: new { obj.Name });
                obj.Id = planTypeInserted.Single();
                return obj;
            }
            else
            {
                query = @"UPDATE PLAN_TYPES SET NAME = @Name WHERE ID = @Id";
                int affectedLines = PlanModuleDB.ConnectionDB.Execute(query, param: new { obj.Name, obj.Id });
                return affectedLines > 0 ? obj : throw new ArgumentException($"There's no PlanType with id = {obj.Id} in database.");
            }
        }
    }
}
