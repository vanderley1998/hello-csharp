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

        public void GetAll()
        {
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM USERS";
                        var reader = command.ExecuteReader();
                        Console.WriteLine($"Total de usuários: {reader.FieldCount}");
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
