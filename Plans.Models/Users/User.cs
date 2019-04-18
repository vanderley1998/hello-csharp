using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.Users
{
    public class User
    {

        public User()
        {
        }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public User(int id, string name, DateTime registerDate, DateTime lastchangedDate, bool canCreatePlan, bool removed)
        {
            Id = id;
            Name = name;
            RegisterDate = registerDate;
            LastchangedDate = lastchangedDate;
            CanCreatePlan = canCreatePlan;
            Removed = removed;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastchangedDate { get; set; }
        public bool CanCreatePlan { get; set; }
        public bool Removed { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }

    }
}
