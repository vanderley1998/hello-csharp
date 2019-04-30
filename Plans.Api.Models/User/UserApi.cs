using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Plans.Api.Models
{
    [XmlType("User")]
    public class UserApi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanCreatePlan { get; set; }
    }
}
