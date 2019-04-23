using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace Plans.Database
{
    public class DataUser : ICrud<User>
    {
        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> list = PlanModuleDB.OpenConnection().Query<User>("SELECT * FROM USERS");
            return list;
        }

        public bool Delete(int id)
        {
            int affectedLines = PlanModuleDB.OpenConnection().Execute($"UPDATE USERS SET REMOVED = 1 WHERE ID = @Id", new { Id = id });
            return affectedLines > 0;
        }

        public User Save(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
