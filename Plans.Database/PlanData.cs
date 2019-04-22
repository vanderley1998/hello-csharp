using Plans.Models.Plans;
using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Database
{
    public class PlanData
    {
        public SqlConnection Connection { get; private set; }

        public PlanData(SqlConnection conn)
        {
            Connection = conn;
        }

        public List<Plan> GetAll()
        {
            List<Plan> list = new List<Plan>();
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM PLANS";
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Plan plan = new Plan(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                new PlanType(reader.GetInt32(2)),
                                new User(reader.GetInt32(3)),
                                new PlanStatus(reader.GetInt32(4))
                            );
                            if(!reader.IsDBNull(5))
                            {
                                plan.StartDate = reader.GetDateTime(5);
                            }
                            if (!reader.IsDBNull(6))
                            {
                                plan.EndDate = reader.GetDateTime(6);
                            }
                            if (!reader.IsDBNull(8))
                            {
                                plan.Cost = reader.GetDecimal(8);
                            }
                            if (!reader.IsDBNull(7))
                            {
                                plan.Description = reader.GetString(7);
                            }
                            list.Add(plan);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            return list;
        }
    }
}
