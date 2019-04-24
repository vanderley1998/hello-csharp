using Plans.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using Dapper;
using Plans.Models.Users;
using Plans.Models.Plans;

namespace Plans.Database
{
    public class DataPlanInterestedUsers : ICrud<PlanInterestedUser>
    {
        public bool Delete(int idPlan)
        {
            int affectedLines = PlanModuleDB.OpenConnection().Execute($"DELETE FROM PLAN_INTERESTED_USERS WHERE ID_PLAN = @Id", new { Id = idPlan });
            return affectedLines > 0;
        }

        public IEnumerable<PlanInterestedUser> GetAll()
        {
            IEnumerable<PlanInterestedUser> list = PlanModuleDB.OpenConnection()
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

        public PlanInterestedUser Save(PlanInterestedUser obj)
        {
            string query;
            if (obj.Id == 0)
            {
                query = @"
                    INSERT INTO PLAN_INTERESTED_USERS (ID_PLAN, ID_USER)
                    VALUES (@IdPlan, @IdUser);
                    SELECT CAST(SCOPE_IDENTITY() as int);";
                var planTypeInserted = PlanModuleDB.OpenConnection().Query<int>(query, param: new { IdPlan = obj.Plan.Id, IdUser = obj.User.Id });
                obj.Id = planTypeInserted.Single();
                return obj;
            }
            else
            {
                query = @"
                        UPDATE PLAN_INTERESTED_USERS
                        SET ID_PLAN = @IdPlan, ID_USER = @IdUser WHERE ID = @Id";
                int affectedLines = PlanModuleDB.OpenConnection().Execute(query, param: new { obj.Id, IdPlan = obj.Plan.Id, IdUser = obj.User.Id });
                return affectedLines > 0 ? obj : throw new ArgumentException($"There's no PlanInterestedUser with id = {obj.Id} in database.");
            }
        }

        public void Save(List<PlanInterestedUser> list)
        {
            foreach (var item in list)
            {
                Save(item);
            }
        }
    }
}
