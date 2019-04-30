using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Plans.Api.Models
{
    [XmlType("Plan")]
    public class PlanApi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int User { get; set; }
        public int Status { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public List<int> InterestedUsers { get; set; }
    }
}
