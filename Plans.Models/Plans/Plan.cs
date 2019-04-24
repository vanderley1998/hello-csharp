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

        public Plan(int id)
        {
            this.Id = id;
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

        public Plan(int id, string name, PlanType planType, User user, PlanStatus planStatus, DateTime sTART_DATE, DateTime eND_DATE) : this(id, name)
        {
            Type = planType;
            User = user;
            Status = planStatus;
            StartDate = sTART_DATE;
            EndDate = eND_DATE;
        }

        public Plan(string name, PlanType type, User user, PlanStatus status, DateTime startDate, DateTime endDate, string description, decimal cost)
        {
            Name = name;
            Type = type;
            User = user;
            Status = status;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            Cost = cost;
        }

        public Plan(int id, string name, PlanType type, User user, PlanStatus status, DateTime startDate, DateTime endDate, string description, decimal cost)
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
        public decimal Cost { get; set; }
        public List<User> InterestedUsers { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Plan other)
            {
                return other.Id == this.Id;
            }
            throw new ArgumentNullException("Expected type Plan but passed " + obj.GetType());
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"\t[Id: {Id}, Name: {Name}, User: {User.Name} (ID: {User.Id})]";
        }
    }
}
