using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Api.Models.Extensions
{
    public static class UserHistoryExtensions
    {
        public static UserHistoryApi ToUserHistoryApi(this UserHistory userHistory)
        {
            return new UserHistoryApi
            {
                Id = userHistory.Id,
                User = userHistory.User.Id,
                Status = userHistory.Status,
                CreateNewPlan = userHistory.CreateNewPlan,
                Date = userHistory.Date.ToString("yyyy-MM-dd")
            };
        }
    }
}
