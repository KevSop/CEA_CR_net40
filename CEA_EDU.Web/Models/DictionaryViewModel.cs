using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Web.Models
{
    public class DictionaryViewModel
    {
        #region Constructor
        public DictionaryViewModel() { }

        public DictionaryViewModel(Int32 id, String code, String name, String parentCode, String type, String value, Int32? sortValue, String description, String isDisplay, String valid, DateTime createTime, String createBy, DateTime updateTime, String updateBy)
        {
            this.id = id;
            this.code = code;
            this.name = name;
            this.parentCode = parentCode;
            this.type = type;
            this.value = value;
            this.sortValue = sortValue;
            this.description = description;
            this.isDisplay = isDisplay;
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
        private String code;

        public String Code
        {
            get { return this.code; }
            set { this.code = value; }
        }
        private String name;

        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        private String parentCode;

        public String ParentCode
        {
            get { return this.parentCode; }
            set { this.parentCode = value; }
        }
        private String type;

        public String Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        private String value;

        public String Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private Int32? sortValue;

        public Int32? SortValue
        {
            get { return this.sortValue; }
            set { this.sortValue = value; }
        }
        private String description;

        public String Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        private String isDisplay;

        public String IsDisplay
        {
            get { return this.isDisplay; }
            set { this.isDisplay = value; }
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
            if (string.IsNullOrEmpty(this.Code))
            {
                validatorResult = false;
                this.ErrorList.Add("The Code should not be empty!");
            }
            if (this.Code != null && 50 < this.Code.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Code should not be greater then 50!");
            }
            if (string.IsNullOrEmpty(this.Name))
            {
                validatorResult = false;
                this.ErrorList.Add("The Name should not be empty!");
            }
            if (this.Name != null && 50 < this.Name.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Name should not be greater then 50!");
            }
            if (string.IsNullOrEmpty(this.ParentCode))
            {
                validatorResult = false;
                this.ErrorList.Add("The ParentCode should not be empty!");
            }
            if (this.ParentCode != null && 50 < this.ParentCode.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of ParentCode should not be greater then 50!");
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
            if (string.IsNullOrEmpty(this.Value))
            {
                validatorResult = false;
                this.ErrorList.Add("The Value should not be empty!");
            }
            if (this.Value != null && 50 < this.Value.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Value should not be greater then 50!");
            }
            if (this.Description != null && 100 < this.Description.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Description should not be greater then 100!");
            }
            if (this.IsDisplay != null && 1 < this.IsDisplay.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of IsDisplay should not be greater then 1!");
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
