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
        private static readonly PlanModuleDB _planModuleDB = new PlanModuleDB(ConfigurationManager.ConnectionStrings["PlansModule"].ToString());

        private static readonly DictionaryUsers _dictionaryUsers = new DictionaryUsers();
        private static readonly DictionaryPlans _dictionaryPlans = new DictionaryPlans();

        private static List<Plan> _listPlans;
        private static List<User> _listUsers;
        private static List<PlanStatus> _listPlanStatus;
        private static List<PlanType> _listPlanTypes;
        private static List<PlanHistory> _listPlansHistory;
        private static List<UserHistory> _listUsersHistory;

        static List<Plan> GetPlansByUser(int idUser)
        {
            List<Plan> list = new List<Plan>();
            Console.WriteLine($"=> Planos por responsável (ID: {idUser})");
            foreach (var plan in _listPlans
                .Where(plan => plan.User.Id == idUser)
                .OrderBy(plan => plan.Id)
            )
            {
                list.Add(plan);
            }
            return list;
        }

        static void GetPlansFromDB()
        {
            IEnumerable<Plan> qPlans = _planModuleDB.DataPlan.GetAll();
            _listPlans = qPlans.ToList();
            foreach (var item in qPlans.ToList())
            {
                _dictionaryPlans.InsertData(item);
            }
        }

        static void GetUsersFromDB()
        {
            IEnumerable<User> qUsers = _planModuleDB.DataUser.GetAll();
            _listUsers = qUsers.ToList();
            foreach (var item in qUsers.ToList())
            {
                _dictionaryUsers.InsertData(item);
            }
        }

        static void GetPlanStatusFromDB()
        {
            IEnumerable<PlanStatus> qPlanStatus = _planModuleDB.DataPlanStatus.GetAll();
            _listPlanStatus = qPlanStatus.ToList();
        }

        static void GetPlanTypesFromDB()
        {
            IEnumerable<PlanType> qPlanTypes = _planModuleDB.DataPlanType.GetAll();
            _listPlanTypes = qPlanTypes.ToList();
        }

        static void GetPlansHistoryFromDB()
        {
            IEnumerable<PlanHistory> qPlansHistory = _planModuleDB.DataPlansHistory.GetAll();
            _listPlansHistory = qPlansHistory.ToList();
        }

        static void GetUsersHistoryFromDB()
        {
            IEnumerable<UserHistory> qUsersHistory = _planModuleDB.DataUsersHistory.GetAll();
            _listUsersHistory = qUsersHistory.ToList();
        }

        static void Main(string[] args)
        {
            _planModuleDB.DataPlanStatus.Save(new PlanStatus("Teste de inserção C#"));
            Console.ReadLine();
        }

    }

}
