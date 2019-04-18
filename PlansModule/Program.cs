using Plans.Database;
using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlansModule
{
    class Program
    {
        static void Main(string[] args)
        {
            SetUsers();
            Console.ReadLine();
        }

        static void SetUsers()
        {
            DictionaryUsers dictionaryUsers = new DictionaryUsers();
            dictionaryUsers.CreateUser(new User(1, "Vanderley"));
            dictionaryUsers.CreateUser(new User(2, "Joao"));
            dictionaryUsers.CreateUser(new User(3, "Luiz"));
            dictionaryUsers.CreateUser(new User(4, "Antonio"));
            dictionaryUsers.ListUsers();
        }

        public static void TestConnection()
        {
            try
            {
                using (var connection = Connection.OpenConnection())
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
