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

        private static DictionaryUsers _dictionaryUsers = new DictionaryUsers();
        private static DictionaryPlans _dictionaryPlans = new DictionaryPlans();
        private static PlanModuleDB _planModuleDB = new PlanModuleDB(ConfigurationManager.ConnectionStrings["PlansModule"].ToString());

        static void SetDictionaryUsers()
        {
            Console.WriteLine("===== Dicionário de Usuários =====".ToUpper());
            _dictionaryUsers.InsertData(new User(1, "Vanderley"));
            _dictionaryUsers.InsertData(new User(2, "Joao"));
            _dictionaryUsers.InsertData(new User(3, "Luiz"));
            _dictionaryUsers.InsertData(new User(4, "Antonio"));
            _dictionaryUsers.ShowData();
        }

        static void SetDictionaryPlans()
        {
            Console.WriteLine("===== Dicionário de Planos =====".ToUpper());
            _dictionaryPlans.InsertData(new Plan(1, "Treinamento do pessoal do atendimento", _dictionaryUsers.GetItem(1)));
            _dictionaryPlans.InsertData(new Plan(2, "Planejamento estratégico do comercial", _dictionaryUsers.GetItem(2)));
            _dictionaryPlans.InsertData(new Plan(3, "Workshop de SQL Server para o time do Dev", _dictionaryUsers.GetItem(3)));
            _dictionaryPlans.InsertData(new Plan(4, "Semana da Computação - VII Edição", _dictionaryUsers.GetItem(4)));
            _dictionaryPlans.ShowData();
        }

        static void Main(string[] args)
        {
            try
            {
                SetDictionaryUsers();
                SetDictionaryPlans();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            _planModuleDB.UserData.GetAll();
            Console.ReadLine();
        }

    }

}
