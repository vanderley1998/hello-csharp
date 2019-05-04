using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.Plans
{
    public class InnerPlan
    {
        public int Id { get; set; }
        public int ParentPlan { get; set; }
        public int ChildPlan { get; set; }
    }
}
