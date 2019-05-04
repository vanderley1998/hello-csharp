using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Plan's name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Plan's type id is required.")]
        public int Type { get; set; }

        [Required(ErrorMessage = "Plan's user id is required.")]
        public int User { get; set; }

        public int Status { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public List<int> InterestedUsers { get; set; }
        public List<int> InnerPlans { get; set; }
    }
}
