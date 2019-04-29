using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Api.Models
{
    public class PlanHistoryApi
    {
        public int Id { get; set; }
        public int Plan { get; set; }
        public int PlanStatus { get; set; }
        public string Date { get; set; }
    }
}
