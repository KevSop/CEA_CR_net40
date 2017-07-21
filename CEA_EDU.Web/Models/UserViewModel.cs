using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA_EDU.Web.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Group { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public string PositionID { get; set; }
        public string PositionName { get; set; }
        public string Sex { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
