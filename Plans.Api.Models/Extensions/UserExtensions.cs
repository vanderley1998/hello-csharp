using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Api.Models.Extensions
{
    public static class UserExtensions
    {
        public static UserApi ToUserApi(this User user)
        {
            return new UserApi
            {
                Id = user.Id,
                Name = user.Name,
                RegisterDate = user.RegisterDate.ToString("yyyy-MM-dd"),
                CanCreatePlan = user.CanCreatePlan
            };
        }

        public static User ToUser(this UserApi userApi)
        {
            return new User
            {
                Id = userApi.Id,
                Name = userApi.Name,
                RegisterDate = DateTime.Parse(userApi.RegisterDate),
                CanCreatePlan = userApi.CanCreatePlan
            };
        }
    }
}
