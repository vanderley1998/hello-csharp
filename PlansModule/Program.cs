using Plans.Database;
using Plans.Models.Dictionary;
using Plans.Models.Plans;
using Plans.Models.Users;
using Plans.Models.View;
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
        private static List<InterestedUsersByPlan> _listInterestedUsersByPlan;
        private static List<TotalPlansByUser> _listTotalPlansByUser;
        private static List<PlanByUser> _listPlanByUser;

        static void GetPlansByUser(int idUser)
        {
            Console.WriteLine($"=> Planos por responsável (ID: {idUser})");
            foreach (var plan in _listPlans.ToList().Where(plan => plan.User.Id == idUser).OrderBy(plan => plan.Id))
            {
                Console.WriteLine(plan);
            }
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

        static void GetInterestedUsersByPlanDB()
        {
            var qInterestedUsersByPlanHistory = _planModuleDB.ViewInterestedUsersByPlan.GetAll();
            _listInterestedUsersByPlan = qInterestedUsersByPlanHistory.ToList();
        }

        static void GetTotalPlansByUserDB()
        {
            var qTotalPlansByUser = _planModuleDB.ViewTotalPlansByUser.GetAll();
            _listTotalPlansByUser = qTotalPlansByUser.ToList();
        }

        static void GetPlanByUserDB()
        {
            var qPlanByUser = _planModuleDB.ViewPlansByUsers.GetAll();
            _listPlanByUser = qPlanByUser.ToList();
        }

        static void Delete<TCrud, TObj>(ICrud<TCrud> crud, TObj obj)
        {
            try
            {
                Console.Write("Id do dado a ser removido: ");
                int id = int.Parse(Console.ReadLine());
                bool flag = crud.Delete(id);
                if(flag == true)
                {
                    Console.WriteLine("Removido com sucesso!");
                    if (obj is PlanStatus) { _listPlanStatus.RemoveAll(plan => plan.Id == id); }
                    if (obj is PlanType) { _listPlanTypes.RemoveAll(type => type.Id == id); }
                    if (obj is Plan)
                    {
                        _listPlans.RemoveAll(plan => plan.Id == id);
                        _dictionaryPlans.ListData.Remove(id);
                    }
                    if (obj is User)
                    {
                        _listUsers.ToList().Find(user => user.Id == id).Removed = true;
                        _dictionaryUsers.GetItem(id).Removed = true;
                    }
                }
                else { Console.WriteLine("Erro ao deletar!"); }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao deletar!");
                Console.WriteLine(e.StackTrace);
                throw e;
            }
        }

        static void CreateUser()
        {
            User user = new User();

            Console.Write("Nome do novo usuário: ");
            user.Name = Console.ReadLine();

            user.RegisterDate = DateTime.Now;
            user.LastchangedDate = user.RegisterDate;
            user.CanCreatePlan = true;
            user.Removed = false;

            User insertedUser = _planModuleDB.DataUser.Save(user);
            if(insertedUser != null)
            {
                Console.WriteLine("Inserido com sucesso!");
                _listUsers.Add(insertedUser);
                _dictionaryUsers.InsertData(insertedUser);
            }
        }

        static void CreatePlan()
        {
            Plan plan = new Plan();

            Console.Write("Nome do novo plano.......: ");
            plan.Name = Console.ReadLine();
            Console.Write("ID do tipo do novo plano.: ");
            plan.Type = new PlanType(int.Parse(Console.ReadLine()));
            Console.Write("ID do usuário responsável: ");
            plan.User = new User(int.Parse(Console.ReadLine()));
            Console.Write("Data de início...........: ");
            plan.StartDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Data de término..........: ");
            plan.EndDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Descrição................: ");
            plan.Description = Console.ReadLine();
            Console.Write("Custo....................: R$");
            plan.Cost = decimal.Parse(Console.ReadLine());

            plan.Status = new PlanStatus(1);

            Plan insertedUser = _planModuleDB.DataPlan.Save(plan);
            if (insertedUser != null)
            {
                Console.WriteLine("Inserido com sucesso!");
                _listPlans.Add(insertedUser);
                _dictionaryPlans.InsertData(insertedUser);
            }
        }

        static void CreatePlanType()
        {
            PlanType planType = new PlanType();

            Console.Write("Nome do novo tipo: ");
            planType.Name = Console.ReadLine();

            PlanType insertedType = _planModuleDB.DataPlanType.Save(planType);
            if (insertedType != null)
            {
                Console.WriteLine("Inserido com sucesso!");
                _listPlanTypes.Add(insertedType);
            }
        }

        static void CreatePlanStatus()
        {
            PlanStatus planStatus = new PlanStatus();

            Console.Write("Nome do novo status: ");
            planStatus.Name = Console.ReadLine();

            PlanStatus insertedStatus = _planModuleDB.DataPlanStatus.Save(planStatus);
            if (insertedStatus != null)
            {
                Console.WriteLine("Inserido com sucesso!");
                _listPlanStatus.Add(insertedStatus);
            }
        }

        static void UpdateUser()
        {
            Console.Write("Id do usuário: ");
            int id = int.Parse(Console.ReadLine());
            User user = _dictionaryUsers.GetItem(id);
            Console.WriteLine(user);

            Console.Write("Nome...................: ");
            user.Name = Console.ReadLine();

            Console.Write("Pode criar planos (true, false): ");
            user.CanCreatePlan = bool.Parse(Console.ReadLine());

            User updatedUser = _planModuleDB.DataUser.Save(user);
            if (updatedUser != null)
            {
                Console.WriteLine("Editado com sucesso!");
                User userLocal = _listUsers.ToList().Find(u => u.Id == id);
                userLocal.Name = updatedUser.Name;
                userLocal.CanCreatePlan = userLocal.CanCreatePlan;
                _dictionaryUsers.GetItem(id).Name = updatedUser.Name;
                _dictionaryUsers.GetItem(id).CanCreatePlan = updatedUser.CanCreatePlan;
            }
        }

        static void UpdatePlan()
        {
            Console.Write("Id do plano: ");
            int id = int.Parse(Console.ReadLine());
            Plan plan = _dictionaryPlans.GetItem(id);
            Console.WriteLine(plan);

            Console.Write("Nome do plano.......: ");
            plan.Name = Console.ReadLine();
            Console.Write("ID do tipo do plano.: ");
            plan.Type = new PlanType(int.Parse(Console.ReadLine()));
            Console.Write("ID do usuário responsável: ");
            plan.User = new User(int.Parse(Console.ReadLine()));
            Console.Write("Data de início...........: ");
            plan.StartDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Data de término..........: ");
            plan.EndDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Descrição................: ");
            plan.Description = Console.ReadLine();
            Console.Write("Custo....................: R$");
            plan.Cost = decimal.Parse(Console.ReadLine());
            Console.Write("ID do status.............: ");
            plan.Status = new PlanStatus(int.Parse(Console.ReadLine()));

            Plan updatedPlan = _planModuleDB.DataPlan.Save(plan);
            if (updatedPlan != null)
            {
                Console.WriteLine("Editado com sucesso!");
                _listPlans.RemoveAll(p => p.Id == id);
                _listPlans.Add(updatedPlan);
                _dictionaryPlans.ListData.Remove(updatedPlan.Id);
                _dictionaryPlans.InsertData(updatedPlan);
            }
        }

        static void UpdatePlanType()
        {
            Console.Write("Id do tipo: ");
            int id = int.Parse(Console.ReadLine());
            PlanType type = _listPlanTypes.Find(t => t.Id == id);
            Console.WriteLine(type);

            Console.Write("Nome: ");
            type.Name = Console.ReadLine();

            PlanType updatedPlanType = _planModuleDB.DataPlanType.Save(type);
            if (updatedPlanType != null)
            {
                Console.WriteLine("Editado com sucesso!");
                PlanType planTypeLocal = _listPlanTypes.ToList().Find(t => t.Id == id);
                planTypeLocal.Name = updatedPlanType.Name;
            }
        }

        static void UpdatePlanStatus()
        {
            Console.Write("Id do status: ");
            int id = int.Parse(Console.ReadLine());
            PlanStatus status = _listPlanStatus.Find(s => s.Id == id);
            Console.WriteLine(status);

            Console.Write("Nome: ");
            status.Name = Console.ReadLine();

            PlanStatus updatedPlanStatus = _planModuleDB.DataPlanStatus.Save(status);
            if (updatedPlanStatus != null)
            {
                Console.WriteLine("Editado com sucesso!");
                PlanStatus planStatusLocal = _listPlanStatus.ToList().Find(s => s.Id == id);
                planStatusLocal.Name = updatedPlanStatus.Name;
            }
        }

        static void Init()
        {
            GetUsersFromDB();
            GetPlansFromDB();
            GetPlansHistoryFromDB();
            GetPlanStatusFromDB();
            GetPlanTypesFromDB();
            GetUsersHistoryFromDB();
            GetInterestedUsersByPlanDB();
            GetTotalPlansByUserDB();
            GetPlanByUserDB();
        }

        static void MainMenu()
        {
            string op;
            do
            {
                Console.Clear();
                Console.WriteLine("================= MENU =================");
                Console.WriteLine("1. Usuários");
                Console.WriteLine("2. Planos");
                Console.WriteLine("3. Tipos de planos");
                Console.WriteLine("4. Status de planos");
                Console.WriteLine("-1. Sair");
                Console.WriteLine("========================================");
                Console.Write("Opção: ");
                op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        SubMenu("Usuários");
                        break;
                    case "2":
                        SubMenu("Planos");
                        break;
                    case "3":
                        SubMenu("Tipos de planos");
                        break;
                    case "4":
                        SubMenu("Status de planos");
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

        static void SubMenu(string title)
        {
            string op;
            do
            {
                Console.Clear();
                Console.WriteLine($"================= {title.ToUpper()} =============");
                Console.WriteLine("1. Editar");
                Console.WriteLine("2. Cadastrar novo");
                Console.WriteLine("3. Exibir");
                Console.WriteLine("4. Remover");
                Console.WriteLine("-1. Voltar");
                Console.WriteLine("========================================");
                Console.Write("Opção: ");
                op = Console.ReadLine();
                MenuAction(title, op);
            } while (!op.Equals("-1"));
        }

        static void MenuAction(string title, string op)
        {
            if (title.Equals("Usuários"))
            {
                switch (op)
                {
                    case "1":
                        Console.Clear();
                        UpdateUser();
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        CreateUser();
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        _dictionaryUsers.ShowData();
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        Delete(_planModuleDB.DataUser, new User());
                        Console.ReadKey();
                        break;
                }
            }
            if (title.Equals("Planos"))
            {
                switch (op)
                {
                    case "1":
                        Console.Clear();
                        UpdatePlan();
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        CreatePlan();
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        _dictionaryPlans.ShowData();
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        Delete(_planModuleDB.DataPlan, new Plan());
                        Console.ReadKey();
                        break;
                }
            }
            if (title.Equals("Tipos de planos"))
            {
                switch (op)
                {
                    case "1":
                        Console.Clear();
                        UpdatePlanType();
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        CreatePlanType();
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
                        Delete(_planModuleDB.DataPlanType, new PlanType());
                        Console.ReadKey();
                        break;
                }
            }
            if (title.Equals("Status de planos"))
            {
                switch (op)
                {
                    case "1":
                        Console.Clear();
                        UpdatePlanStatus();
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        CreatePlanStatus();
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        foreach (var item in _listPlanStatus.ToList())
                        {
                            Console.WriteLine(item);
                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        Delete(_planModuleDB.DataPlanStatus, new PlanStatus());
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Carregamento...");
            Init();
            Console.Clear();
            MainMenu();
        }

    }

}
