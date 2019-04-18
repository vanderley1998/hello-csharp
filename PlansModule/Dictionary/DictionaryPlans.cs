using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlansModule.Dictionary
{
    class DictionaryPlans : IDictionaryList<Plan>
    {
        public IDictionary<int, Plan> ListData { get; } = new Dictionary<int, Plan>();

        public void InsertData(Plan plan)
        {
            ListData.Add(plan.Id, plan);
        }

        public void ShowData()
        {
            foreach (var plan in this.ListData)
            {
                Console.WriteLine(plan);
            }
        }
    }
}
