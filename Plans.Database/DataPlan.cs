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
            string query;
            if (obj.Id == 0)
            {
                query = @"
                    INSERT INTO PLANS (NAME, ID_TYPE, ID_USER, ID_STATUS, START_DATE, END_DATE, DESCRIPTION, COST)
                    VALUES (@Name, @IdType, @IdUser, 1, @StartDate, @EndDate, @Description, @Cost);
                    SELECT CAST(SCOPE_IDENTITY() as int);";
                var planTypeInserted = PlanModuleDB.OpenConnection()
                    .Query<int>(query, param: new { obj.Name,  IdType = obj.Type.Id, IdUser = obj.User.Id, obj.StartDate, obj.EndDate, obj.Description, obj.Cost});
                obj.Id = planTypeInserted.Single();
                return obj;
            }
            else
            {
                query = @"UPDATE PLANS SET NAME = @Name, ID_TYPE = @IdType, ID_STATUS = @IdStatus, ID_USER = @IdUser, START_DATE = @StartDate, END_DATE = @EndDate, DESCRIPTION = @Description, COST = @Cost WHERE ID = @Id";
                int affectedLines = PlanModuleDB.OpenConnection().Execute(query, param: new { obj.Id, obj.Name, IdType = obj.Type.Id, IdStatus = obj.Status.Id, IdUser = obj.User.Id, obj.StartDate, obj.EndDate, obj.Description, obj.Cost });
                return affectedLines > 0 ? obj : throw new ArgumentException($"There's no Plan with id = {obj.Id} in database.");
            }
        }
    }
}
