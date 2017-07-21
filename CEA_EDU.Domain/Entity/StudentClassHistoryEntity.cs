using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Domain.Entity
{
    [Table("StudentClassHistory")]
    public class StudentClassHistoryEntity
    {
        #region Constructor
        public StudentClassHistoryEntity() { }

        public StudentClassHistoryEntity(Int32 id,Int32 studentID,Int32 arrangeClassID,DateTime? startTime,DateTime? endTime,Int32? score,String isAttend,String isPass,String remark,String valid,DateTime createTime,String createBy,DateTime updateTime,String updateBy)
        {
            this.id = id;
            this.studentID = studentID;
            this.arrangeClassID = arrangeClassID;
            this.startTime = startTime;
            this.endTime = endTime;
            this.score = score;
            this.isAttend = isAttend;
            this.isPass = isPass;
            this.remark = remark;
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
        private Int32 studentID;

        public Int32 StudentID
        {
            get { return this.studentID; }
            set { this.studentID = value; }
        }
        private Int32 arrangeClassID;

        public Int32 ArrangeClassID
        {
            get { return this.arrangeClassID; }
            set { this.arrangeClassID = value; }
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
        private Int32? score;

        public Int32? Score
        {
            get { return this.score; }
            set { this.score = value; }
        }
        private String isAttend;

        public String IsAttend
        {
            get { return this.isAttend; }
            set { this.isAttend = value; }
        }
        private String isPass;

        public String IsPass
        {
            get { return this.isPass; }
            set { this.isPass = value; }
        }
        private String remark;

        public String Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
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
            if (this.IsAttend != null && 1 < this.IsAttend.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of IsAttend should not be greater then 1!");
            }
            if (this.IsPass != null && 1 < this.IsPass.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of IsPass should not be greater then 1!");
            }
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
