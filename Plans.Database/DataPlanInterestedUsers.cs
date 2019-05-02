using Plans.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using Dapper;
using Plans.Models.Users;
using Plans.Models.Plans;
using System.Data.SqlClient;

namespace Plans.Database
{
    public class DataPlanInterestedUsers : ICrud<PlanInterestedUser>
    {
        public static bool Delete(int idPlan, SqlCommand command)
        {
            int affectedLines = command.Connection.Execute($"DELETE FROM PLAN_INTERESTED_USERS WHERE ID_PLAN = @Id", new { Id = idPlan }, command.Transaction);
            return affectedLines > 0;
        }

        public PlanInterestedUser Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlanInterestedUser> GetAll()
        {
            IEnumerable<PlanInterestedUser> list = PlanModuleDB.ConnectionDB
                .Query<PlanInterestedUser, User, Plan, PlanInterestedUser>(@"
                    SELECT * FROM PLAN_INTERESTED_USERS PIU
                    INNER JOIN USERS U ON U.ID = PIU.ID_PLAN
                    INNER JOIN PLANS P ON P.ID = PIU.ID_USER",
                map: (interestedUser, user, plan) =>
                {
                    interestedUser.Plan = plan;
                    interestedUser.User = user;
                    return interestedUser;
                }
                );
            return list;
        }

        public IEnumerable<PlanInterestedUser> GetById(int id)
        {
            IEnumerable<PlanInterestedUser> list = PlanModuleDB.ConnectionDB
                .Query<PlanInterestedUser, User, Plan, PlanInterestedUser>(@"
                    SELECT * FROM PLAN_INTERESTED_USERS PIU
                    INNER JOIN USERS U ON U.ID = PIU.ID_PLAN
                    INNER JOIN PLANS P ON P.ID = PIU.ID_USER
                    WHERE P.ID = @id", param: new { id },
                map: (interestedUser, user, plan) =>
                {
                    interestedUser.Plan = plan;
                    interestedUser.User = user;
                    return interestedUser;
                }
                );
            return list;
        }

        public PlanInterestedUser Save(PlanInterestedUser obj)
        {
            string query;
            if (obj.Id == 0)
            {
                query = @"
                    INSERT INTO PLAN_INTERESTED_USERS (ID_PLAN, ID_USER)
                    VALUES (@IdPlan, @IdUser);
                    SELECT CAST(SCOPE_IDENTITY() as int);";
                var planTypeInserted = PlanModuleDB.ConnectionDB.Query<int>(query, param: new { IdPlan = obj.Plan.Id, IdUser = obj.User.Id });
                obj.Id = planTypeInserted.Single();
                return obj;
            }
            else
            {
                query = @"
                    UPDATE PLAN_INTERESTED_USERS
                    SET ID_PLAN = @IdPlan, ID_USER = @IdUser WHERE ID = @Id";
                int affectedLines = PlanModuleDB.ConnectionDB.Execute(query, param: new { obj.Id, IdPlan = obj.Plan.Id, IdUser = obj.User.Id });
                return affectedLines > 0 ? obj : throw new ArgumentException($"There's no PlanInterestedUser with id = {obj.Id} in database.");
            }
        }

        public static void Save(List<PlanInterestedUser> list, SqlCommand command)
        {
            foreach (var planInterestedUser in list)
            {
                Save(planInterestedUser, command);
            }
        }

        internal static PlanInterestedUser Save(PlanInterestedUser obj, SqlCommand command)
        {
            string query;
            if (obj.Id == 0)
            {
                query = @"
                    INSERT INTO PLAN_INTERESTED_USERS (ID_PLAN, ID_USER)
                    VALUES (@IdPlan, @IdUser);
                    SELECT CAST(SCOPE_IDENTITY() as int);";
                var planTypeInserted = command.Connection.Query<int>(query, param: new { IdPlan = obj.Plan.Id, IdUser = obj.User.Id }, command.Transaction);
                obj.Id = planTypeInserted.Single();
                return obj;
            }
            else
            {
                query = @"
                    UPDATE PLAN_INTERESTED_USERS
                    SET ID_PLAN = @IdPlan, ID_USER = @IdUser WHERE ID = @Id";
                int affectedLines = command.Connection.Execute(query, param: new { obj.Id, IdPlan = obj.Plan.Id, IdUser = obj.User.Id }, command.Transaction);
                return affectedLines > 0 ? obj : throw new ArgumentException($"There's no PlanInterestedUser with id = {obj.Id} in database.");
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
