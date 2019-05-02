using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Plans.Models.View;

namespace Plans.Database.View
{
    public class ViewInterestedUsersByPlan : ICrud<InterestedUsersByPlan>
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public InterestedUsersByPlan Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InterestedUsersByPlan> GetAll()
        {
            IEnumerable<InterestedUsersByPlan> list = PlanModuleDB.ConnectionDB.Query<InterestedUsersByPlan>("SELECT * FROM VIEW_INTERESTED_USERS_BY_PLAN");
            return list;
        }

        public IEnumerable<InterestedUsersByPlan> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public InterestedUsersByPlan Save(InterestedUsersByPlan obj)
        {
            throw new NotImplementedException();
        }
    }
}
