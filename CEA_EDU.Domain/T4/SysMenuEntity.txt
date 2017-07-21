using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Domain.Entity
{
    [Table("SysMenu")]
    public class SysMenuEntity
    {
        #region Constructor
        public SysMenuEntity() { }

        public SysMenuEntity(String gUID,String pARENTID,String nAME,String uRL,DateTime createdOn,String createdBy,DateTime updatedOn,String updatedBy,Int32 status,Int32 isDeleted)
        {
            this.gUID = gUID;
            this.pARENTID = pARENTID;
            this.nAME = nAME;
            this.uRL = uRL;
            this.createdOn = createdOn;
            this.createdBy = createdBy;
            this.updatedOn = updatedOn;
            this.updatedBy = updatedBy;
            this.status = status;
            this.isDeleted = isDeleted;
        }
        #endregion

        #region Attributes
        private String gUID;

        public String GUID
        {
            get { return this.gUID; }
            set { this.gUID = value; }
        }
        private String pARENTID;

        public String PARENTID
        {
            get { return this.pARENTID; }
            set { this.pARENTID = value; }
        }
        private String nAME;

        public String NAME
        {
            get { return this.nAME; }
            set { this.nAME = value; }
        }
        private String uRL;

        public String URL
        {
            get { return this.uRL; }
            set { this.uRL = value; }
        }
        private DateTime createdOn;

        public DateTime CreatedOn
        {
            get { return this.createdOn; }
            set { this.createdOn = value; }
        }
        private String createdBy;

        public String CreatedBy
        {
            get { return this.createdBy; }
            set { this.createdBy = value; }
        }
        private DateTime updatedOn;

        public DateTime UpdatedOn
        {
            get { return this.updatedOn; }
            set { this.updatedOn = value; }
        }
        private String updatedBy;

        public String UpdatedBy
        {
            get { return this.updatedBy; }
            set { this.updatedBy = value; }
        }
        private Int32 status;

        public Int32 Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        private Int32 isDeleted;

        public Int32 IsDeleted
        {
            get { return this.isDeleted; }
            set { this.isDeleted = value; }
        }
        #endregion

        #region Validator
        public List<string> ErrorList = new List<string>();
        private bool Validator()
        {    
            bool validatorResult = true;
            if (string.IsNullOrEmpty(this.GUID))
            {
                validatorResult = false;
                this.ErrorList.Add("The GUID should not be empty!");
            }
            if (this.GUID != null && 36 < this.GUID.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of GUID should not be greater then 36!");
            }
            if (string.IsNullOrEmpty(this.PARENTID))
            {
                validatorResult = false;
                this.ErrorList.Add("The PARENTID should not be empty!");
            }
            if (this.PARENTID != null && 36 < this.PARENTID.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of PARENTID should not be greater then 36!");
            }
            if (string.IsNullOrEmpty(this.NAME))
            {
                validatorResult = false;
                this.ErrorList.Add("The NAME should not be empty!");
            }
            if (this.NAME != null && 50 < this.NAME.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of NAME should not be greater then 50!");
            }
            if (string.IsNullOrEmpty(this.URL))
            {
                validatorResult = false;
                this.ErrorList.Add("The URL should not be empty!");
            }
            if (this.URL != null && 50 < this.URL.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of URL should not be greater then 50!");
            }
            if (this.CreatedOn==null)
            {
                validatorResult = false;
                this.ErrorList.Add("The CreatedOn should not be empty!");
            }
            if (string.IsNullOrEmpty(this.CreatedBy))
            {
                validatorResult = false;
                this.ErrorList.Add("The CreatedBy should not be empty!");
            }
            if (this.CreatedBy != null && 20 < this.CreatedBy.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of CreatedBy should not be greater then 20!");
            }
            if (this.UpdatedOn==null)
            {
                validatorResult = false;
                this.ErrorList.Add("The UpdatedOn should not be empty!");
            }
            if (string.IsNullOrEmpty(this.UpdatedBy))
            {
                validatorResult = false;
                this.ErrorList.Add("The UpdatedBy should not be empty!");
            }
            if (this.UpdatedBy != null && 20 < this.UpdatedBy.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of UpdatedBy should not be greater then 20!");
            }
            return validatorResult;
        }    
        #endregion
    }
}
