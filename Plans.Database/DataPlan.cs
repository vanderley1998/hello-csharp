using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Plans.Models.Users;
using System.Data.SqlClient;
using Plans.Models;

namespace Plans.Database
{
    public class DataPlan : ICrud<Plan>
    {
        public bool Delete(int id)
        {
            try
            {
                int affectedLines = PlanModuleDB.ConnectionDB.Execute(
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
                var planFound = PlanModuleDB.ConnectionDB
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
            IEnumerable<Plan> list = PlanModuleDB.ConnectionDB
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

        public IEnumerable<Plan> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Plan Save(Plan plan)
        {
            try
            {
                PlanModuleDB.ConnectionDB.Open();
                SqlCommand command = PlanModuleDB.ConnectionDB.CreateCommand();
                SqlTransaction transaction = PlanModuleDB.ConnectionDB.BeginTransaction();
                command.Connection = PlanModuleDB.ConnectionDB;
                command.Transaction = transaction;
                string query;
                var interestedUsers = plan.InterestedUsers.Select(i => new PlanInterestedUser { Plan = plan, User = i }).ToList();
                if (plan.Id == 0)
                {
                    query = @"
                        INSERT INTO PLANS (NAME, ID_TYPE, ID_USER, ID_STATUS, START_DATE, END_DATE, DESCRIPTION, COST)
                        VALUES (@Name, @IdType, @IdUser, 1, @StartDate, @EndDate, @Description, @Cost);
                        SELECT CAST(SCOPE_IDENTITY() as int);";
                    var planTypeInserted = command.Connection
                        .Query<int>(query, param: new { plan.Name, IdType = plan.Type.Id, IdUser = plan.User.Id, plan.StartDate, plan.EndDate, plan.Description, plan.Cost }, command.Transaction);
                    plan.Id = planTypeInserted.Single();
                    DataPlanInterestedUsers.Save(interestedUsers, command);
                    command.Transaction.Commit();
                    return plan;
                }
                else
                {
                    query = @"
                        UPDATE PLANS
                        SET 
                            NAME = @Name, 
                            ID_TYPE = @IdType, 
                            ID_STATUS = @IdStatus, 
                            ID_USER = @IdUser, 
                            START_DATE = @StartDate, 
                            END_DATE = @EndDate, 
                            DESCRIPTION = @Description, 
                            COST = @Cost 
                        WHERE ID = @Id";
                    int affectedLines = command.Connection.Execute(query, param: new { plan.Id, plan.Name, IdType = plan.Type.Id, IdStatus = plan.Status.Id, IdUser = plan.User.Id, plan.StartDate, plan.EndDate, plan.Description, plan.Cost }, command.Transaction);
                    DataPlanInterestedUsers.Delete(plan.Id, command);
                    DataPlanInterestedUsers.Save(interestedUsers, command);
                    command.Transaction.Commit();
                    return affectedLines > 0 ? plan : throw new ArgumentException($"There's no Plan with id = {plan.Id} in database.");
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                PlanModuleDB.ConnectionDB.Close();
            }
        }
    }
}
