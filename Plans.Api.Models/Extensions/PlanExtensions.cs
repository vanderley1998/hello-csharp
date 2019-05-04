using Plans.Models.Plans;
using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plans.Api.Models.Extensions
{
    public static class PlanExtensions
    {
        public static PlanApi ToPlanApi(this Plan plan)
        {
            List<int> interestedUsers;
            try
            {
                interestedUsers = plan.InterestedUsers.Select(u => u.Id).ToList();
            }
            catch (ArgumentNullException)
            {
                interestedUsers = new List<int>();
            }
            return new PlanApi
            {
                Id = plan.Id,
                Name = plan.Name,
                Type = plan.Type.Id,
                User = plan.User.Id,
                Status = plan.Status.Id,
                StartDate = plan.StartDate.ToString("yyyy-MM-dd"),
                EndDate = plan.EndDate.ToString("yyyy-MM-dd"),
                Description = plan.Description,
                Cost = plan.Cost,
                InterestedUsers = interestedUsers
            };
        }

        public static Plan ToPlan(this PlanApi planApi)
        {
            List<User> interestedUsers;
            try
            {
                interestedUsers = planApi.InterestedUsers.Select(u => new User { Id = u}).ToList();
            }
            catch (ArgumentNullException)
            {
                interestedUsers = new List<User>();
            }
            if (string.IsNullOrEmpty(planApi.StartDate)) { planApi.StartDate = "3000-01-01 00:00:00"; }
            if (string.IsNullOrEmpty(planApi.EndDate)) { planApi.EndDate = "3000-01-01 00:00:00"; }
            return new Plan
            {
                Id = planApi.Id,
                Name = planApi.Name,
                Type = new PlanType { Id = planApi.Type },
                User = new User { Id = planApi.User },
                Status = new PlanStatus { Id = planApi.Status},
                StartDate = DateTime.Parse(planApi.StartDate),
                EndDate = DateTime.Parse(planApi.EndDate),
                Description = planApi.Description,
                Cost = planApi.Cost,
                InterestedUsers = interestedUsers
            };
        }
    }
}
