using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Plans.Models.Users;
using System.Data.SqlClient;

namespace Plans.Database
{
    public class DataPlan : ICrud<Plan>
    {
        public bool Delete(int id)
        {
            try
            {
                int affectedLines = PlanModuleDB.OpenConnection().Execute(
                    $"EXEC DELETE_PLAN_REGISTERS_FROM_ALL_TABLES @ID = @Id", new { Id = id }
                );
                return affectedLines > 0;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public Plan Get(int id)
        {
            try
            {
                var planFound = PlanModuleDB.OpenConnection()
                    .Query<Plan, User, PlanStatus, PlanType, Plan>(@"
                        SELECT
                            P.ID, P.NAME,
                            P.START_DATE as StartDate,
                            P.END_DATE as EndDate,
                            P.DESCRIPTION, P.COST, PT.ID, PT.NAME, PS.ID, PS.NAME, U.ID, U.NAME
                        FROM PLANS P
                        INNER JOIN USERS U ON U.ID = P.ID_USER
                        INNER JOIN PLAN_STATUS PS ON PS.ID = P.ID_STATUS
                        INNER JOIN PLAN_TYPES PT ON PT.ID = P.ID_TYPE
                        WHERE P.ID = @id
                    ", param: new { id },
                        map: (plan, user, status, type) =>
                        {
                            plan.User = user;
                            plan.Status = status;
                            plan.Type = type;
                            return plan;
                        }
                    );
                return planFound.First();
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }

        public IEnumerable<Plan> GetAll()
        {
            IEnumerable<Plan> list = PlanModuleDB.OpenConnection()
                .Query<Plan, User, PlanStatus, PlanType, Plan>(@"
                    SELECT
                        P.ID, P.NAME,
                        P.START_DATE as StartDate,
                        P.END_DATE as EndDate,
                        P.DESCRIPTION, P.COST, PT.ID, PT.NAME, PS.ID, PS.NAME, U.ID, U.NAME
                    FROM PLANS P
                    INNER JOIN USERS U ON U.ID = P.ID_USER
                    INNER JOIN PLAN_STATUS PS ON PS.ID = P.ID_STATUS
                    INNER JOIN PLAN_TYPES PT ON PT.ID = P.ID_TYPE
                ",
                    map: (plan, user, status, type) =>
                    {
                        plan.User = user;
                        plan.Status = status;
                        plan.Type = type;
                        return plan;
                    }
                );
            return list;
        }

        public Plan Save(Plan obj)
        {
            try
            {
                string query;
                if (obj.Id == 0)
                {
                    query = @"
                    INSERT INTO PLANS (NAME, ID_TYPE, ID_USER, ID_STATUS, START_DATE, END_DATE, DESCRIPTION, COST)
                    VALUES (@Name, @IdType, @IdUser, 1, @StartDate, @EndDate, @Description, @Cost);
                    SELECT CAST(SCOPE_IDENTITY() as int);";
                    var planTypeInserted = PlanModuleDB.OpenConnection()
                        .Query<int>(query, param: new { obj.Name, IdType = obj.Type.Id, IdUser = obj.User.Id, obj.StartDate, obj.EndDate, obj.Description, obj.Cost });
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
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}
