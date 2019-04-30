using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Api.Models
{
    public class UserHistoryApi
    {
        public int Id { get; set; }
        public int User { get; set; }
        public bool Status { get; set; }
        public bool CreateNewPlan { get; set; }
        public string Date { get; set; }
    }
}
