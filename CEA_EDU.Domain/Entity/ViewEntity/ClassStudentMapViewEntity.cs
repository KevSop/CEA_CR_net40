using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Domain.Entity.ViewEntity
{
    public class ClassStudentMapViewEntity
    {
        public Int32 ID { get; set; }

        public Int32 ClassID { get; set; }

        public String ClassCode { get; set; }

        public String ClassName { get; set; }

        public String ClassType { get; set; }

        public Int32 StudentID { get; set; }

        public String StudentCode { get; set; }

        public String StudentName { get; set; }
    }
}
