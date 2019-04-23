using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.Plans
{
    public class PlanStatus
    {

        public PlanStatus()
        {
        }

        public PlanStatus(string name)
        {
            Name = name;
        }

        public PlanStatus(int id)
        {
            Id = id;
        }

        public PlanStatus(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"[Id: {Id}, Name: {Name}]";
        }

    }
}
