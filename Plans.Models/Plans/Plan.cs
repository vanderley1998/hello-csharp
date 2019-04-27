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

        private DateTime _startDate;
        private DateTime _endDate;

        public Plan()
        {
        }

        public Plan(int id, string name, PlanType planType, User user, PlanStatus planStatus, DateTime START_DATE, DateTime END_DATE)
        {
            Type = planType;
            User = user;
            Status = planStatus;
            StartDate = START_DATE;
            EndDate = END_DATE;
        }

        public Plan(int id, string name, PlanType type, User user, PlanStatus status, DateTime START_DATE, DateTime END_DATE, string description, decimal cost)
        {
            Id = id;
            Name = name;
            Type = type;
            User = user;
            Status = status;
            StartDate = START_DATE;
            EndDate = END_DATE;
            Description = description;
            Cost = cost;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public PlanType Type { get; set; }
        public User User { get; set; }
        public PlanStatus Status { get; set; }
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if(value.CompareTo(DateTime.Parse("3000-01-01 00:00:00")) == 0)
                {
                    return;
                }
                _startDate = value;
            }
        }
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (value.CompareTo(DateTime.Parse("3000-01-01 00:00:00")) == 0)
                {
                    return;
                }
                _endDate = value;
            }
        }
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
            return $"\t[Id: {Id}, Name: {Name}, User: {User.Name} (ID: {User.Id}), StartDate: {StartDate}, EndDate: {EndDate}]";
        }
    }
}
