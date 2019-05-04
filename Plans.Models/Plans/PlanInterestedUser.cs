using Plans.Models.Plans;
using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models
{
    public class PlanInterestedUser
    {
        public int Id { get; set; }
        public Plan Plan { get; set; }
        public User User { get; set; }

        public override string ToString()
        {
            return $"[Id: {Id}, Plan: {Plan.Id}, User: {User}]";
        }
    }
}
