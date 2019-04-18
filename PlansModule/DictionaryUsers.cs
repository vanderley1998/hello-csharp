using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlansModule
{
    class DictionaryUsers
    {
        public IDictionary<int, User> Users { get; } = new Dictionary<int, User>();

        public void CreateUser(User user)
        {
            Users.Add(user.Id, user);
        }

        public void ListUsers()
        {
            foreach (var user in this.Users)
            {
                Console.WriteLine(user);
            }
        }
    }
}
