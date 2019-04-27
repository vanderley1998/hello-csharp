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

        private string _startDate;
        private string _endDate;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int User { get; set; }
        public int Status { get; set; }
        public string StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (value.Equals("0001-01-01"))
                {
                    _startDate = null;
                    return;
                }
                _startDate = value;
            }
        }
        public string EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (value.Equals("0001-01-01"))
                {
                    _endDate = null;
                    return;
                }
                _endDate = value;
            }
        }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public List<int> InterestedUsers { get; set; }
    }
}
