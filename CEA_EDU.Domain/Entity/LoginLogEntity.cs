using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Domain.Entity
{
    [Table("LoginLog")]
    public class LoginLogEntity
    {
        #region Constructor
        public LoginLogEntity() { }

        public LoginLogEntity(Int32 id, String guid, String type, Int32 loginID, String loginName, String loginType, String action, DateTime? timeRecord, String remark, String machineID, String loginIP, String loginIP2, DateTime? startTime, DateTime? endTime, DateTime createTime, DateTime updateTime)
        {
            this.id = id;
            this.guid = guid;
            this.type = type;
            this.loginID = loginID;
            this.loginName = loginName;
            this.loginType = loginType;
            this.action = action;
            this.timeRecord = timeRecord;
            this.remark = remark;
            this.machineID = machineID;
            this.loginIP = loginIP;
            this.loginIP2 = loginIP2;
            this.startTime = startTime;
            this.endTime = endTime;
            this.createTime = createTime;
            this.updateTime = updateTime;
        }
        #endregion

        #region Attributes
        private Int32 id;

        public Int32 ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private String guid;

        public String Guid
        {
            get { return this.guid; }
            set { this.guid = value; }
        }
        private String type;

        public String Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        private Int32 loginID;

        public Int32 LoginID
        {
            get { return this.loginID; }
            set { this.loginID = value; }
        }
        private String loginName;

        public String LoginName
        {
            get { return this.loginName; }
            set { this.loginName = value; }
        }
        private String loginType;

        public String LoginType
        {
            get { return this.loginType; }
            set { this.loginType = value; }
        }
        private String action;

        public String Action
        {
            get { return this.action; }
            set { this.action = value; }
        }
        private DateTime? timeRecord;

        public DateTime? TimeRecord
        {
            get { return this.timeRecord; }
            set { this.timeRecord = value; }
        }
        private String remark;

        public String Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
        private String machineID;

        public String MachineID
        {
            get { return this.machineID; }
            set { this.machineID = value; }
        }
        private String loginIP;

        public String LoginIP
        {
            get { return this.loginIP; }
            set { this.loginIP = value; }
        }
        private String loginIP2;

        public String LoginIP2
        {
            get { return this.loginIP2; }
            set { this.loginIP2 = value; }
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
        private DateTime createTime;

        public DateTime CreateTime
        {
            get { return this.createTime; }
            set { this.createTime = value; }
        }
        private DateTime updateTime;

        public DateTime UpdateTime
        {
            get { return this.updateTime; }
            set { this.updateTime = value; }
        }
        #endregion

        #region Validator
        public List<string> ErrorList = new List<string>();
        private bool Validator()
        {
            bool validatorResult = true;
            if (this.Guid != null && 50 < this.Guid.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Guid should not be greater then 50!");
            }
            if (string.IsNullOrEmpty(this.Type))
            {
                validatorResult = false;
                this.ErrorList.Add("The Type should not be empty!");
            }
            if (this.Type != null && 20 < this.Type.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Type should not be greater then 20!");
            }
            if (string.IsNullOrEmpty(this.LoginName))
            {
                validatorResult = false;
                this.ErrorList.Add("The LoginName should not be empty!");
            }
            if (this.LoginName != null && 255 < this.LoginName.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of LoginName should not be greater then 255!");
            }
            if (string.IsNullOrEmpty(this.LoginType))
            {
                validatorResult = false;
                this.ErrorList.Add("The LoginType should not be empty!");
            }
            if (this.LoginType != null && 20 < this.LoginType.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of LoginType should not be greater then 20!");
            }
            if (this.Action != null && 100 < this.Action.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Action should not be greater then 100!");
            }
            if (this.Remark != null && 500 < this.Remark.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Remark should not be greater then 500!");
            }
            if (this.MachineID != null && 50 < this.MachineID.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of MachineID should not be greater then 50!");
            }
            if (this.LoginIP != null && 50 < this.LoginIP.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of LoginIP should not be greater then 50!");
            }
            if (this.LoginIP2 != null && 50 < this.LoginIP2.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of LoginIP2 should not be greater then 50!");
            }
            if (this.CreateTime == null)
            {
                validatorResult = false;
                this.ErrorList.Add("The CreateTime should not be empty!");
            }
            if (this.UpdateTime == null)
            {
                validatorResult = false;
                this.ErrorList.Add("The UpdateTime should not be empty!");
            }
            return validatorResult;
        }
        #endregion
    }
}
