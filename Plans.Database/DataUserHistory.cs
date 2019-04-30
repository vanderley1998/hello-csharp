using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

namespace Plans.Database
{
    public class DataUserHistory : ICrud<UserHistory>
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserHistory Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserHistory> GetAll()
        {
            IEnumerable<UserHistory> list = PlanModuleDB.ConnectionDB
                .Query<UserHistory, User, UserHistory>(@"
                    SELECT *
                    FROM USERS_HISTORY UH
                    INNER JOIN USERS U ON U.ID = UH.ID_USER",
                map: (userHistory, user) =>
                {
                    userHistory.User = user;
                    return userHistory;
                });
            return list;
        }

        public IEnumerable<UserHistory> GetById(int id)
        {
            IEnumerable<UserHistory> list = PlanModuleDB.ConnectionDB
                .Query<UserHistory, User, UserHistory>(@"
                    SELECT *
                    FROM USERS_HISTORY UH
                    INNER JOIN USERS U ON U.ID = UH.ID_USER
                    WHERE U.ID = @id", param: new { id },
                map: (userHistory, user) =>
                {
                    userHistory.User = user;
                    return userHistory;
                });
            return list;
        }

        public UserHistory Save(UserHistory obj)
        {
            throw new NotImplementedException();
        }
    }
}
