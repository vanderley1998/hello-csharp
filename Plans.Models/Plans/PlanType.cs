using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.Plans
{
    public class PlanType
    {
        public PlanType()
        {
        }

        public PlanType(int id)
        {
            Id = id;
        }

        public PlanType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
