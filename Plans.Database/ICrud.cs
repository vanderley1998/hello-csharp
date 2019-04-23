using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Database
{
    interface ICrud<T>
    {
        IEnumerable<T> GetAll();
        bool Delete(int id);
        T Save(T obj);
    }
}
