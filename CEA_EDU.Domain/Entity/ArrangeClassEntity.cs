using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Domain.Entity
{
    [Table("ArrangeClass")]
    public class ArrangeClassEntity
    {
        #region Constructor
        public ArrangeClassEntity() { }

        public ArrangeClassEntity(Int32 id,Int32 curriculumID,Int32 classID,Int32 classRoomID,Int32 teacherID,DateTime? startTime,DateTime? endTime,String remark,Int32? bespeakCount,Int32? attendCount,Int32? passedCount,String valid,DateTime createTime,String createBy,DateTime updateTime,String updateBy)
        {
            this.id = id;
            this.curriculumID = curriculumID;
            this.classID = classID;
            this.classRoomID = classRoomID;
            this.teacherID = teacherID;
            this.startTime = startTime;
            this.endTime = endTime;
            this.remark = remark;
            this.bespeakCount = bespeakCount;
            this.attendCount = attendCount;
            this.passedCount = passedCount;
            this.valid = valid;
            this.createTime = createTime;
            this.createBy = createBy;
            this.updateTime = updateTime;
            this.updateBy = updateBy;
        }
        #endregion

        #region Attributes
        private Int32 id;

        public Int32 ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private Int32 curriculumID;

        public Int32 CurriculumID
        {
            get { return this.curriculumID; }
            set { this.curriculumID = value; }
        }
        private Int32 classID;

        public Int32 ClassID
        {
            get { return this.classID; }
            set { this.classID = value; }
        }
        private Int32 classRoomID;

        public Int32 ClassRoomID
        {
            get { return this.classRoomID; }
            set { this.classRoomID = value; }
        }
        private Int32 teacherID;

        public Int32 TeacherID
        {
            get { return this.teacherID; }
            set { this.teacherID = value; }
        }
        private DateTime? startTime;

        public DateTime? StartTime
        {
            get { return this.startTime; }
            set { this.startTime = value; }
        }
        private DateTime? endTime;

        public DateTime? EndTime
        {
            get { return this.endTime; }
            set { this.endTime = value; }
        }
        private String remark;

        public String Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
        private Int32? bespeakCount;

        public Int32? BespeakCount
        {
            get { return this.bespeakCount; }
            set { this.bespeakCount = value; }
        }
        private Int32? attendCount;

        public Int32? AttendCount
        {
            get { return this.attendCount; }
            set { this.attendCount = value; }
        }
        private Int32? passedCount;

        public Int32? PassedCount
        {
            get { return this.passedCount; }
            set { this.passedCount = value; }
        }
        private String valid;

        public String Valid
        {
            get { return this.valid; }
            set { this.valid = value; }
        }
        private DateTime createTime;

        public DateTime CreateTime
        {
            get { return this.createTime; }
            set { this.createTime = value; }
        }
        private String createBy;

        public String CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }
        private DateTime updateTime;

        public DateTime UpdateTime
        {
            get { return this.updateTime; }
            set { this.updateTime = value; }
        }
        private String updateBy;

        public String UpdateBy
        {
            get { return this.updateBy; }
            set { this.updateBy = value; }
        }
        #endregion

        #region Validator
        public List<string> ErrorList = new List<string>();
        private bool Validator()
        {    
            bool validatorResult = true;
            if (this.Remark != null && 500 < this.Remark.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Remark should not be greater then 500!");
            }
            if (string.IsNullOrEmpty(this.Valid))
            {
                validatorResult = false;
                this.ErrorList.Add("The Valid should not be empty!");
            }
            if (this.Valid != null && 1 < this.Valid.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Valid should not be greater then 1!");
            }
            if (this.CreateTime==null)
            {
                validatorResult = false;
                this.ErrorList.Add("The CreateTime should not be empty!");
            }
            if (this.CreateBy != null && 20 < this.CreateBy.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of CreateBy should not be greater then 20!");
            }
            if (this.UpdateTime==null)
            {
                validatorResult = false;
                this.ErrorList.Add("The UpdateTime should not be empty!");
            }
            if (this.UpdateBy != null && 20 < this.UpdateBy.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of UpdateBy should not be greater then 20!");
            }
            return validatorResult;
        }    
        #endregion
    }
}
