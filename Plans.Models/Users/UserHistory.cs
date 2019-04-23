using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.Users
{
    public class UserHistory
    {
        public UserHistory()
        {
        }

        public UserHistory(int id, User user, bool status, bool createNewPlan, DateTime date)
        {
            Id = id;
            User = user;
            Status = status;
            CreateNewPlan = createNewPlan;
            Date = date;
        }

        public int Id { get; set; }
        public User User { get; set; }
        public bool Status { get; set; }
        public bool CreateNewPlan { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"[Id: {Id}, User: {User.Id}, Status: {Status}, CreateNewPlan: {CreateNewPlan}, Date: {Date}]";
        }
    }
}
