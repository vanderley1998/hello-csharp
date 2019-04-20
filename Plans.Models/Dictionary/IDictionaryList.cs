using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.Dictionary
{
    internal interface IDictionaryList<T>
    {
        IDictionary<int, T> ListData { get; }

        void InsertData(T obj);
        void ShowData();
        T GetItem(int id);
    }
}
