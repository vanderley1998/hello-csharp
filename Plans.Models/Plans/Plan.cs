using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plans.Models.Users;

namespace Plans.Models.Plans
{
    public class Plan
    {

        public Plan()
        {
        }

        public Plan(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Plan(int id, string name, User user)
        {
            Id = id;
            Name = name;
            User = user;
        }

        public Plan(int id, string name, PlanType type, User user, PlanStatus status, DateTime startDate, DateTime endDate, string description, double cost)
        {
            Id = id;
            Name = name;
            Type = type;
            User = user;
            Status = status;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            Cost = cost;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public PlanType Type { get; set; }
        public User User { get; set; }
        public PlanStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }


        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, User: {User.Name} (ID: {User.Id})";
        }
    }
}
