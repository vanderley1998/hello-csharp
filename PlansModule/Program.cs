using Plans.Database;
using Plans.Models.Dictionary;
using Plans.Models.Plans;
using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

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
            Console.WriteLine($"=> Planos por responsável (ID: {18})");
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

        static void ShowMenu()
        {
            Console.WriteLine("================= MENU =================");
            Console.WriteLine("1. Exibir dicionário de usuários");
            Console.WriteLine("2. Exibir dicionário de planos");
            Console.WriteLine("3. Exibir lista de tipos de planos");
            Console.WriteLine("4. Exibir lista de status de planos");
            Console.WriteLine("5. Exibir lista de histórico de planos");
            Console.WriteLine("6. Exibir lista de histórico de usuários");
            Console.WriteLine("7. Exibir planos por usuário especifico");
            Console.WriteLine("-1. Sair");
            Console.WriteLine("========================================");
        }
 
        static void Init()
        {
            GetUsersFromDB();
            GetPlansFromDB();
            GetPlansHistoryFromDB();
            GetPlanStatusFromDB();
            GetPlanTypesFromDB();
            GetUsersHistoryFromDB();
        }

        static void Main(string[] args)
        {
            Init();
            string op;
            do
            {
                Console.Clear();
                ShowMenu();
                Console.Write("Opção: ");
                op = Console.ReadLine();
                switch(op)
                {
                    case "1":
                        Console.Clear();
                        _dictionaryUsers.ShowData();
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        _dictionaryPlans.ShowData();
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        foreach (var item in _listPlanTypes.ToList())
                        {
                            Console.WriteLine(item);
                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        foreach (var item in _listPlanStatus.ToList())
                        {
                            Console.WriteLine(item);
                        }
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.Clear();
                        foreach (var item in _listPlansHistory.ToList())
                        {
                            Console.WriteLine(item);
                        }
                        Console.ReadKey();
                        break;
                    case "6":
                        Console.Clear();
                        foreach (var item in _listUsersHistory.ToList())
                        {
                            Console.WriteLine(item);
                        }
                        Console.ReadKey();
                        break;
                    case "7":
                        Console.Clear();
                        Console.Write("Digite o Id do usuário: ");
                        int id = Console.Read();
                        foreach (var item in GetPlansByUser(id))
                        {
                            Console.WriteLine(item);
                        }
                        Console.ReadKey();
                        break;
                    case "-1":
                        Console.WriteLine("Bye!");
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
            while (!op.Equals("-1"));
        }

    }

}
