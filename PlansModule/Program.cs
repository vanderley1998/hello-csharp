using Plans.Database;
using Plans.Models.Users;
using PlansModule.Dictionary;
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
            SetDictionaryUsers();
            Console.ReadLine();
        }

        static void SetDictionaryUsers()
        {
            DictionaryUsers dictionaryUsers = new DictionaryUsers();
            dictionaryUsers.InsertData(new User(1, "Vanderley"));
            dictionaryUsers.InsertData(new User(2, "Joao"));
            dictionaryUsers.InsertData(new User(3, "Luiz"));
            dictionaryUsers.InsertData(new User(4, "Antonio"));
            dictionaryUsers.ShowData();
        }

        //public static void TestConnection()
        //{
        //    try
        //    {
        //        using (var connection = Connection.OpenConnection())
        //        {
        //            connection.Open();
        //            using (var command = connection.CreateCommand())
        //            {
        //                command.CommandText = "SELECT * FROM USERS";
        //                var reader = command.ExecuteReader();
        //                Console.WriteLine($"Total de usuários: {reader.FieldCount}");
        //                Console.ReadLine();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}
    }

}
