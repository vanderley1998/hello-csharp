using Plans.Models.Exception;
using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.Dictionary
{
    public class DictionaryUsers : IDictionaryList<User>
    {
        public IDictionary<int, User> ListData { get; } = new Dictionary<int, User>();

        public void InsertData(User user)
        {
            ListData.Add(user.Id, user);
        }

        public void ShowData()
        {
            foreach (var user in this.ListData)
            {
                Console.WriteLine(user);
            }
        }

        public User GetItem(int id)
        {
            try
            {
                var value = ListData[id];
                return value;
            }
            catch (KeyNotFoundException)
            {
                throw new UserNotFoundException($"ERROR: User with Id = {id} not found.");
            }
        }
    }
}
