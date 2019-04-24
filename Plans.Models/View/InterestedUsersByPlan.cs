using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.View
{
    public class InterestedUsersByPlan
    {
        public InterestedUsersByPlan(int ID, string PLAN_NAME, string USER_NAME, DateTime START_DATE, DateTime END_DATE, string INTERESTED_USERS)
        {
            Id = ID;
            PlanName = PLAN_NAME;
            Username = USER_NAME;
            StartDate = START_DATE;
            EndDate = END_DATE;
            InterestedUsers = INTERESTED_USERS;
        }

        public int Id { get; set; }
        public string PlanName { get; set; }
        public string Username { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InterestedUsers { get; set; }

        public override string ToString()
        {
            return $"[Id: {Id}, PlanName: {PlanName}, UserName: {Username}, StartDate: {StartDate}, EndDate: {EndDate},\nInterestedUsers: {InterestedUsers}]\n";
        }
    }
}
