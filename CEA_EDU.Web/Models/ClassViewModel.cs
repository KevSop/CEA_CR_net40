using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA_EDU.Web.Models
{
    public class ClassViewModel
    {
        public int ClassID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? TeacherID { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public string TeacherCode { get; set; }
        public string TeacherName { get; set; }


    }
}
