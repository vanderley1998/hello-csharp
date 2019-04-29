using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Api.Models.Extensions
{
    public static class PlanHistoryExtensions
    {
        public static PlanHistoryApi ToPlanHistoryApi(this PlanHistory planHistory)
        {
            return new PlanHistoryApi
            {
                Id = planHistory.Id,
                Plan = planHistory.Plan.Id,
                PlanStatus = planHistory.PlanStatus.Id,
                Date = planHistory.Date.ToString("yyyy-MM-dd")
            };
        }
    }
}
