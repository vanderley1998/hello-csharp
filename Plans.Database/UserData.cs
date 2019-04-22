using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Database
{
    public class UserData
    {

        public SqlConnection Connection { get; private set; }

        public UserData(SqlConnection conn)
        {
            Connection = conn;
        }

        public List<User> GetAll()
        {
            List<User> list = new List<User>();
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM USERS";
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            User user = new User(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetDateTime(2),
                                reader.GetDateTime(3),
                                reader.GetBoolean(4),
                                reader.GetBoolean(5)
                            );
                            list.Add(user);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }

    }
}
