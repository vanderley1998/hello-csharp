using Plans.Models.Plans;
using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Api.Models.Extensions
{
    public static class PlanExtensions
    {
        public static PlanApi ToPlanApi(this Plan plan)
        {
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
                Cost = plan.Cost
            };
        }

        public static Plan ToPlan(this PlanApi planApi)
        {
            DateTime startDate;
            DateTime endDate;

            if (string.IsNullOrEmpty(planApi.StartDate))
            {
                startDate = DateTime.Parse("3000-01-01 00:00:00");
                Console.WriteLine(startDate);
            }
            if (string.IsNullOrEmpty(planApi.StartDate))
            {
                endDate = DateTime.Parse("3000-01-01 00:00:00");
                Console.WriteLine(endDate);
            }

            return new Plan
            {
                Id = planApi.Id,
                Name = planApi.Name,
                Type = new PlanType(planApi.Type),
                User = new User(planApi.User),
                Status = new PlanStatus(planApi.Status),
                StartDate = startDate,
                EndDate = endDate,
                Description = planApi.Description,
                Cost = planApi.Cost
            };
        }
    }
}
