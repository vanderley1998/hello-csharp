using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

namespace Plans.Database
{
    public class DataUsersHistory : ICrud<UserHistory>
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserHistory> GetAll()
        {
            IEnumerable<UserHistory> list = PlanModuleDB.OpenConnection()
                .Query<UserHistory, User, UserHistory>("SELECT * FROM USERS_HISTORY UH INNER JOIN USERS U ON U.ID = UH.ID",
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
