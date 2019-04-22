using Plans.Database;
using Plans.Models.Dictionary;
using Plans.Models.Plans;
using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlansModule
{

    class Program
    {

        private static readonly DictionaryUsers _dictionaryUsers = new DictionaryUsers();
        private static readonly DictionaryPlans _dictionaryPlans = new DictionaryPlans();
        private static readonly ISet<Plan> _listPlans = new HashSet<Plan>();
        private static readonly ISet<User> _listUsers = new HashSet<User>();
        private static readonly PlanModuleDB _planModuleDB = new PlanModuleDB(ConfigurationManager.ConnectionStrings["PlansModule"].ToString());

        static void SetDictionaryUsers()
        {
            Console.WriteLine("=> Dicionário de Usuários".ToUpper());
            _dictionaryUsers.ShowData();
        }

        static void SetDictionaryPlans()
        {
            Console.WriteLine("=> Dicionário de Planos".ToUpper());
            _dictionaryPlans.ShowData();
        }

        public static void SetListPlans()
        {
            Console.WriteLine("=> Lista de Planos".ToUpper());
            foreach (var item in _listPlans)
            {
                Console.WriteLine($"{item}");
            }
        }

        public static void SetListUsers()
        {
            Console.WriteLine("=> Lista de Usuários".ToUpper());
            foreach (var item in _listUsers)
            {
                Console.WriteLine($"{item}");
            }
        }

        public static void GetPlansByUser(int idUser)
        {
            Console.WriteLine($"=> Planos por responsável (ID: {idUser})");
            foreach (var plan in _listPlans
                .Where(plan => plan.User.Id == idUser)
                .OrderBy(plan => plan.Id)
            )
            {
                Console.WriteLine(plan);
            }
        }

        public static void GetDataFromDB()
        {
            // Get data from users
            foreach (var user in _planModuleDB.UserData.GetAll())
            {
                _listUsers.Add(user);
                _dictionaryUsers.InsertData(user);
            }

            // Get data from plans
            foreach (var plan in _planModuleDB.PlanData.GetAll())
            {
                _listPlans.Add(plan);
                _dictionaryPlans.InsertData(plan);
            }
        }

        static void Main(string[] args)
        {
            GetDataFromDB();
            try
            {
                Console.WriteLine("===== Dicionários =====");
                SetDictionaryUsers();
                SetDictionaryPlans();
                Console.WriteLine("\n\n===== Listas (ISet) =====");
                SetListPlans();
                SetListUsers();
                Console.WriteLine("\n\n============ Manipulando os dados (Linq) =============");
                GetPlansByUser(14);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            Console.ReadLine();
        }

    }

}
