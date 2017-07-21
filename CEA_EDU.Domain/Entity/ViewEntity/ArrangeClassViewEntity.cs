using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Domain.Entity.ViewEntity
{
    public class ArrangeClassViewEntity
    {
        public Int32 ID { get; set; }

        public Int32 CurriculumID { get; set; }

        public String CurriculumCode { get; set; }

        public String CurriculumName { get; set; }

        public Int32 ClassID { get; set; }

        public String ClassCode { get; set; }

        public String ClassName { get; set; }

        public Int32 ClassRoomID { get; set; }

        public String ClassRoomCode { get; set; }

        public String ClassRoomName { get; set; }

        public Int32 TeacherID { get; set; }

        public String TeacherCode { get; set; }

        public String TeacherName { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public String Remark { get; set; }

        public int? BespeakCount { get; set; }

        public int? AttendCount { get; set; }

        public int? PassedCount { get; set; }

    }
}
