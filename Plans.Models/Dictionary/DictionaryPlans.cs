using Plans.Models.Plans;
using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.Dictionary
{
    public class DictionaryPlans : IDictionaryList<Plan>
    {
        public IDictionary<int, Plan> ListData { get; } = new Dictionary<int, Plan>();

        public Plan GetItem(int id)
        {
            try
            {
                var value = ListData[id];
                return value;
            }
            catch (KeyNotFoundException e)
            {
                throw e;
            }
        }

        public void InsertData(Plan plan)
        {
            ListData.Add(plan.Id, plan);
        }

        public void ShowData()
        {
            foreach (var plan in this.ListData)
            {
                Console.WriteLine("\t" + plan);
            }
        }
    }
}
