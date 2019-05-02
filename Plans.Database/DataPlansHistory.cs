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
    public class DataPlansHistory : ICrud<PlanHistory>
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PlanHistory Get(int id)
        {
            try
            {
                var userFound = PlanModuleDB.ConnectionDB
                    .Query<PlanHistory>(@"
                        SELECT PH.ID, PH.DATE, P.ID, PS.ID, U.ID, U.NAME
                        FROM PLANS_HISTORY PH 
                        INNER JOIN PLANS P ON P.ID = PH.ID_PLAN 
                        INNER JOIN PLAN_STATUS PS ON PS.ID = PH.ID_PLAN_STATUS 
                        INNER JOIN USERS U ON U.ID = P.ID_USER
                        WHERE P.ID = @id
                    ", param: new { id });
                return userFound.First();
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }

        public IEnumerable<PlanHistory> GetAll()
        {
            IEnumerable<PlanHistory> list = PlanModuleDB.ConnectionDB
                .Query<PlanHistory, Plan, PlanStatus, User, PlanHistory>(@"
                    SELECT PH.ID, PH.DATE, P.ID, PS.ID, U.ID, U.NAME
                    FROM PLANS_HISTORY PH 
                    INNER JOIN PLANS P ON P.ID = PH.ID_PLAN 
                    INNER JOIN PLAN_STATUS PS ON PS.ID = PH.ID_PLAN_STATUS 
                    INNER JOIN USERS U ON U.ID = P.ID_USER",
                map: (planHistory, plan, status, user) =>
                {
                    planHistory.Plan = plan;
                    planHistory.PlanStatus = status;
                    planHistory.Plan.User = user;
                    return planHistory;
                });
            return list;
        }

        public IEnumerable<PlanHistory> GetById(int id)
        {
            IEnumerable<PlanHistory> list = PlanModuleDB.ConnectionDB
                .Query<PlanHistory, Plan, PlanStatus, User, PlanHistory>(@"
                    SELECT PH.ID, PH.DATE, P.ID, PS.ID, U.ID, U.NAME
                    FROM PLANS_HISTORY PH 
                    INNER JOIN PLANS P ON P.ID = PH.ID_PLAN 
                    INNER JOIN PLAN_STATUS PS ON PS.ID = PH.ID_PLAN_STATUS 
                    INNER JOIN USERS U ON U.ID = P.ID_USER
                    WHERE P.ID = @id", param: new { id },
                map: (planHistory, plan, status, user) =>
                {
                    planHistory.Plan = plan;
                    planHistory.PlanStatus = status;
                    planHistory.Plan.User = user;
                    return planHistory;
                });
            return list;
        }

        public PlanHistory Save(PlanHistory obj)
        {
            throw new NotImplementedException();
        }
    }
}
