using Plans.Models.Plans;
using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.View
{
    public class PlanByUser
    {
        public Plan Plan { get; set; }
        public DateTime DateLateStatus { get; set; }

        public override string ToString()
        {
            return $"[User: {Plan.User.Id}, Plan: {Plan.Id}, DateLateStatus: {DateLateStatus}]";
        }
    }
}
